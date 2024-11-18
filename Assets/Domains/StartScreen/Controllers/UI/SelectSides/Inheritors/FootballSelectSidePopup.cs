using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using Data.FootballApi;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Controllers.UI.StartScreen.SelectSides
{
    public class FootballSelectSidePopup : SelectSidesPopup
    {
        [Inject] private FootballDiskProvider _diskProvider;

        [SerializeField] private MatchView matchView;
        [SerializeField] private CanvasGroup loadingIndicator;

        private int _currentPlayersViewIndex;
        private CancellationTokenSource _cts;
        private List<Match> _matches => _diskProvider.GetFixtures();

        [Inject]
        public async UniTaskVoid Construct()
        {
            _cts = new CancellationTokenSource();

            await WaitForDataToLoad(_cts.Token);

            if (_matches.Count > 0)
            {
                await LoadCurrentPlayersView(0, _cts.Token);
                matchView.SetBackgroundImage(_matches.First(), _cts.Token).Forget();
            }
        }

        private async UniTask WaitForDataToLoad(CancellationToken cancellationToken)
        {
            loadingIndicator.alpha = 1;
            await UniTask.WaitUntil(_diskProvider.IsDataLoaded, cancellationToken: cancellationToken);
            LeanTween.alphaCanvas(loadingIndicator, 0, 0.2f)
                .setOnComplete(() => loadingIndicator.gameObject.SetActive(false));
        }

        private async UniTask LoadCurrentPlayersView(int index, CancellationToken cancellationToken)
        {
            if (index < 0 || index >= _matches.Count || cancellationToken.IsCancellationRequested) return;

            if (currentPlayersView != null)
            {
                Destroy(currentPlayersView.gameObject);
            }

            var match = _matches[index];
            var view = await CreateView(match, cancellationToken);

            if (cancellationToken.IsCancellationRequested) return;

            view.gameObject.SetActive(true);
            currentPlayersView = view;
            _currentPlayersViewIndex = index;
        }

        private async UniTask<PlayersView> CreateView(Match match, CancellationToken cancellationToken)
        {
            matchView.Set(match);
            var teamDisks = await _diskProvider.GetDisks(match, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            var strategyOptions = turnStrategyService.GetAllStrategies();

            var config = new PlayersViewRequest.Builder().SetPlayersCount(2)
                .SetDiskOptions(teamDisks)
                .SetIsStrategyInBigBox(false)
                .SetStrategyOptions(strategyOptions)
                .SetParentTransform(viewsTransform)
                .Build();

            return playersViewFactory.Create(config);
        }

        public async void Next()
        {
            if (_matches.Count == 0 || _cts.Token.IsCancellationRequested) return;

            int nextIndex = (_currentPlayersViewIndex + 1) % _matches.Count;
            await LoadCurrentPlayersView(nextIndex, _cts.Token);
        }

        private void OnDestroy()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
}
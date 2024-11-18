using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Data;
using Data.FootballApi;
using Domains.DiskSources.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Controllers.UI.StartScreen.SelectSides
{
    public class FootballSelectSidePopup : SelectSidesPopup
    {
        [Inject] private FootballDiskProvider _diskProvider;

        [SerializeField] private MatchView _matchView;

        private int _currentPlayersViewIndex;
        private List<Match> _matches => _diskProvider.GetFixtures();

        [Inject]
        public async void Construct()
        {
            if (_matches.Count > 0)
            {
                await LoadCurrentPlayersView(0);
                _matchView.SetBackgroundImage(_matches.First()).Forget();

            }
        }

        private async UniTask LoadCurrentPlayersView(int index)
        {
            if (index < 0 || index >= _matches.Count) return;

            if (currentPlayersView != null)
            {
                Destroy(currentPlayersView.gameObject);
            }

            var match = _matches[index];
            var view = await CreateView(match);
            
            view.gameObject.SetActive(true);

            currentPlayersView = view;
            _currentPlayersViewIndex = index;
        }

        private async UniTask<PlayersView> CreateView(Match match)
        {
            _matchView.Set(match);
            var teamDisks = await _diskProvider.GetDisks(match);
            var strategyOptions = turnStrategyService.GetAllStrategies();

            var config = new PlayersViewRequest.Builder()
                .SetPlayersCount(2)
                .SetDiskOptions(teamDisks)
                .SetIsStrategyInBigBox(false)
                .SetStrategyOptions(strategyOptions)
                .SetParentTransform(viewsTransform)
                .Build();

            return playersViewFactory.Create(config);
        }

        public async void Next()
        {
            if (_matches.Count == 0) return;
            int nextIndex = (_currentPlayersViewIndex + 1) % _matches.Count;
            await LoadCurrentPlayersView(nextIndex);
        }
    }
}

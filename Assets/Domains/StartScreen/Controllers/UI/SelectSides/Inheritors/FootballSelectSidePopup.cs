using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Data;
using Data;
using Data.FootballApi;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Utils;
using Zenject;
using Image = UnityEngine.UI.Image;

namespace Controllers.UI.StartScreen.SelectSides
{
    public class FootballSelectSidePopup : SelectSidesPopup
    {
        [Inject] private FootballApiService _footballApiService;
        [Inject] private FootballDiskProvider _diskProvider;

        private List<PlayersView> _views = new();
        private int _currentViewIndex;

        [Inject]
        public async void Construct()
        {
            foreach (var match in _footballApiService.Matches)
            {
                var view = await CreateView(match);
                view.gameObject.SetActive(false);
                _views.Add(view);

                if (_views.Count == 1)
                {
                    currentView = view;
                    _currentViewIndex = 0;
                    currentView.gameObject.SetActive(true);
                }
            }
        }

        private async UniTask<PlayersView> CreateView(Match match)
        {
            var teamDisks = await _diskProvider.GetDisks(match);
            var strategyOptions = turnStrategyService.GetAllStrategies();

            var config = new PlayersViewRequest.Builder().SetPlayersCount(2)
                .SetDiskOptions(teamDisks)
                .SetIsStrategyInBigBox(false)
                .SetStrategyOptions(strategyOptions)
                .SetParentTransform(viewsTransform)
                .Build();

            return playersViewFactory.Create(config);
        }

        public void Next()
        {
            if (_views.Count == 0) return;

            currentView.gameObject.SetActive(false);

            _currentViewIndex = (_currentViewIndex + 1) % _views.Count;
            currentView = _views[_currentViewIndex];

            currentView.gameObject.SetActive(true);
        }
    }
}
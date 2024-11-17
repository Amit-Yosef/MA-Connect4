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


        private List< PlayersConfigurationView > _views = new();
        private int _currentViewIndex;

        [Inject]
        public async void Construct()
        {
            var strategyOptions = turnStrategyService.GetAllStrategies();
            
            foreach (var match in _footballApiService.Matches)
            {
                var teamDisks = await _diskProvider.GetDisks(match);
                
                var config = new PlayersConfigurationViewConfig.Builder()
                    .SetPlayersCount(2)
                    .SetDiskOptions(teamDisks)
                    .SetIsStrategyInBigBox(false)
                    .SetStrategyOptions(strategyOptions)
                    .SetParentTransform(viewsTransform)
                    .Build();
                
                var view = playersViewFactory.Create(config);
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
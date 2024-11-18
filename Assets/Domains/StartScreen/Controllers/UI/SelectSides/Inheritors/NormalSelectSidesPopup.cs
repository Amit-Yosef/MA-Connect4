using Data;
using Zenject;

namespace Controllers.UI.StartScreen.SelectSides
{
    public class NormalSelectSidesPopup : SelectSidesPopup
    {
        [Inject] private NormalDiskProvider _diskProvider;
        [Inject]
        public void Construct()
        {
            var diskOptions = _diskProvider.GetAll();
            var strategyOptions = turnStrategyService.GetAllStrategies();
            var config = new PlayersViewRequest.Builder()
                .SetPlayersCount(2)
                .SetDiskOptions(diskOptions)
                .SetStrategyOptions(strategyOptions)
                .SetParentTransform(viewsTransform)
                .Build();
            
            currentView = playersViewFactory.Create(config);
        }
    }
}
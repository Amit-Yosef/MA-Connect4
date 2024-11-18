using Cysharp.Threading.Tasks;
using Data;
using Domains.DiskSources.Interfaces;
using Zenject;

namespace Controllers.UI.StartScreen.SelectSides
{
    public class NormalSelectSidesPopup : SelectSidesPopup
    {
        [Inject] private NormalDiskProvider _diskProvider;
        [Inject]
        public async UniTaskVoid Construct()
        {
            var diskOptions = await _diskProvider.GetDisks();
            var strategyOptions = turnStrategyService.GetAllStrategies();
            var config = new PlayersViewRequest.Builder()
                .SetPlayersCount(2)
                .SetDiskOptions(diskOptions)
                .SetStrategyOptions(strategyOptions)
                .SetParentTransform(viewsTransform)
                .Build();
            
            currentPlayersView = playersViewFactory.Create(config);
        }
    }
}
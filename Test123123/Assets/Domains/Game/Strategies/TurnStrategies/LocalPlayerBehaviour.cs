using System.Threading;
using Cysharp.Threading.Tasks;
using MoonActive.Connect4;
using Project.Data.Models;
using Project.Utils.ExtensionMethods;
using Zenject;

namespace Game.Strategies.TurnStrategies
{
    public class LocalPlayerBehaviour : PlayerBehaviour
    {
        [Inject] private ConnectGameGrid _grid;
        private PlayerBehaviorData _strategyData;
        public override PlayerBehaviorData GetPlayerData() => _strategyData;
        
        protected override async UniTask<int> SelectColumn(CancellationToken cancellationToken)
        {
            return await _grid.WaitForColumnSelect(cancellationToken);
        }
        

        
    }
    
    
}
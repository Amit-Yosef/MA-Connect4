using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Data.Models;
using Project.Utils;

namespace Game.Strategies.TurnStrategies
{
    public class BotPlayerBehaviour : PlayerBehaviour
    {
        
        private PlayerBehaviorData _strategyData;
        public override PlayerBehaviorData GetPlayerData() => _strategyData;

        protected override  UniTask<int> SelectColumn(CancellationToken cancellationToken)
        {
             return UniTask.FromResult(BoardUtils.GetRandomValidColumn(BoardSystem));
        }


    }
}
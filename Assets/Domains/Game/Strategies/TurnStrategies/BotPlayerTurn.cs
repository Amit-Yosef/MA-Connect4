using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Data.Models;
using Project.Utils;

namespace Game.Strategies.TurnStrategies
{
    public class BotPlayerTurn : PlayerTurn
    {
        
        private PlayerTurnStrategyData _strategyData;
        public override PlayerTurnStrategyData GetPlayerData() => _strategyData;

        protected override async UniTask<int> SelectColumn(CancellationToken cancellationToken)
        {
             return BoardUtills.GetRandomValidColumn(BoardSystem);
        }


    }
}
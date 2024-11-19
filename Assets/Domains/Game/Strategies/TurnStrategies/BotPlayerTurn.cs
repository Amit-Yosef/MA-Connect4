using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using Managers;
using MoonActive.Connect4;
using Utils;
using Zenject;

namespace Controllers
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
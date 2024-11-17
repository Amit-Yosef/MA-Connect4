using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using Managers;
using MoonActive.Connect4;
using Utils;
using Zenject;

namespace Controllers
{
    public class BotPlayerTurnStrategy : PlayerTurnStrategy
    {
        
        private PlayerTurnStrategyData _strategyData;
        public override PlayerTurnStrategyData GetPlayerData() => _strategyData;

        protected override async UniTask<int> SelectColumn(CancellationTokenSource cts)
        {
             return BoardUtills.GetRandomValidColumn(BoardSystem);
        }


    }
}
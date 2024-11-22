using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Strategies.TurnStrategies;
using MoonActive.Connect4;
using Project.Data.Models;
using Zenject;

namespace Game.Controllers.Players
{
    public class PlayerController
    {
        [Inject] private DiskData _disk;
        [Inject] private IPlayerTurnStrategy _turnStrategy;

        public async UniTask DoTurn(CancellationToken cancellationToken)
        {
            await _turnStrategy.DoTurn(_disk, cancellationToken);
        }

        public class Factory : PlaceholderFactory<DiskData, IPlayerTurnStrategy, PlayerController>
        {
        }
    }
}
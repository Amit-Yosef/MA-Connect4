using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using MoonActive.Connect4;
using Zenject;

namespace Controllers.Players
{
    public class Player
    {
        [Inject] private DiskData _disk;
        [Inject] private IPlayerTurnStrategy _turnStrategy;

        public async UniTask DoTurn(CancellationToken cancellationToken)
        {
            await _turnStrategy.DoTurn(_disk, cancellationToken);
        }
        
        public class Factory : PlaceholderFactory<DiskData, IPlayerTurnStrategy, Player>
        {
            
        }
    }
}
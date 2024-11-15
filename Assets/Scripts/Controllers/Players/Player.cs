using Cysharp.Threading.Tasks;
using MoonActive.Connect4;
using Zenject;

namespace Controllers.Players
{
    public class Player
    {
        [Inject] private Disk _disk;
        [Inject] private IPlayerTurnStrategy _turnStrategy;

        public async UniTask DoTurn()
        {
            await _turnStrategy.DoTurn(_disk);
        }
        
        public class Factory : PlaceholderFactory<Disk, IPlayerTurnStrategy, Player>
        {
            
        }
    }
}
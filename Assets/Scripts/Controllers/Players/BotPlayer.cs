using Cysharp.Threading.Tasks;
using Managers;
using MoonActive.Connect4;
using Zenject;

namespace Controllers
{
    public class BotPlayer : BasePlayer
    {
        [Inject]
        private void Construct(Disk diskPrefab)
        {
            DiskPrefab = diskPrefab;
        }
        protected override async UniTask<int> SelectColumn()
        {
             return BoardSystem.GetRandomValidColumn();
        }
        public class Factory : PlaceholderFactory<Disk, BotPlayer>
        {
        }
    }
}
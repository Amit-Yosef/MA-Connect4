using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Managers;
using MoonActive.Connect4;
using Zenject;
using UnityEngine;
using Utils.ExtensionMethods;

namespace Controllers
{
    public class LocalPlayer : BasePlayer
    {
        [Inject] private ConnectGameGrid _grid;

        [Inject]
        private void Construct(Disk diskPrefab)
        {
            DiskPrefab = diskPrefab;
        }

        protected override async UniTask<int> SelectColumn()
        {
            return await _grid.WaitForColumnSelect();
        }

        public class Factory : PlaceholderFactory<Disk, LocalPlayer>
        {
        }
    }
}
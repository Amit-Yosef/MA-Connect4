using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;
using MoonActive.Connect4;
using Zenject;

namespace Controllers
{
    public class NormalDiskProvider
    {
        [Inject] private List<DiskData> _disks;

        public async UniTask<List<DiskData>> GetDisks()
        {
            return _disks;
        }
    }
}
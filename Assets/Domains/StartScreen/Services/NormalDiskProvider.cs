using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;
using Domains.DiskSources.Interfaces;
using MoonActive.Connect4;
using Zenject;

namespace Controllers
{
    public class NormalDiskProvider : IDiskProvider<IDisk>
    {
        [Inject] private List<DiskData> _disks;

        public async UniTask<List<DiskData>> GetDisks()
        {
            return _disks;
        }
    }
}
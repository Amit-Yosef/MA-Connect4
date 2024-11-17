using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;
using Zenject;

namespace Controllers
{
    public class NormalDiskProvider
    {
        [Inject] private List<DiskData> _disks;

        public List<DiskData> GetAll()
        {
            return _disks;
        }
    }
}
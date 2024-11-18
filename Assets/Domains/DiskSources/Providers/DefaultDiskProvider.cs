using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using Domains.DiskSources;
using Domains.DiskSources.Interfaces;
using MoonActive.Connect4;
using Zenject;

namespace Domains.DiskSources.Providers
{
    public class DefaultDiskProvider : BaseDiskProvider
    {
        private List<DiskData> _disks;
        public bool IsDataLoaded => _disks != null;

        public List<DiskData> GetDisks()
        {
            return _disks;
        }


        public async UniTaskVoid Populate(Func<CancellationToken, UniTask<List<DiskData>>> getDisks)
        {
            _disks = await getDisks(_cts.Token);

        }
        
        public void Populate(List<DiskData> disks)
        {
            _disks = disks;
        }
        
        public void Reset()
        {
            _disks = null;
        }
    }
}
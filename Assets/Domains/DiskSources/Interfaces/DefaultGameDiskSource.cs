using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;
using Domains.DiskSources.Providers;
using Zenject;

namespace Domains.DiskSources.Interfaces
{
    public abstract class DefaultGameDiskSource : IInitializable, IDisposable
    {
        [Inject] private DiskProvidersConfiguration _diskProvidersConfiguration;
        [Inject] private DefaultGameDiskProvider _defaultGameDiskProvider;
        
        protected abstract DefaultGameDiskSourceType GetDefaultDiskSource();


        public void Initialize()
        {
            _diskProvidersConfiguration.DefaultGameSourceChanged += DefaultGameSourceChanged;

        }
        
        private async UniTaskVoid DefaultGameSourceChanged(DefaultGameDiskSourceType mode)
        {
            if (mode == GetDefaultDiskSource())
            {
                _defaultGameDiskProvider.Reset();
                var diskDatas = await GetDiskDatas();
                _defaultGameDiskProvider.Populate(diskDatas);
            }

        }
        protected abstract UniTask<List<DiskData>> GetDiskDatas();

        public virtual void Dispose()
        {
            _diskProvidersConfiguration.DefaultGameSourceChanged -= DefaultGameSourceChanged;

        }
    }
}
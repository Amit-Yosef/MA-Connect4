using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using Domains.DiskSources.Controllers;
using Domains.DiskSources.Providers;
using MoonActive.Connect4;
using Zenject;

namespace Domains.DiskSources.Interfaces
{
    public abstract class DefaultGameWebDiskSource : IInitializable, IDisposable , ICacheableDisks
    {
        [Inject] private DiskProvidersConfiguration _diskProvidersConfiguration;
        [Inject] private DefaultGameDiskProvider _defaultGameDiskProvider;

        private CancellationTokenSource _cts = new CancellationTokenSource();
        protected List<DiskData> cachedDisks;
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

                if (cachedDisks != null)
                {
                    _defaultGameDiskProvider.Populate(cachedDisks);
                    return;                    
                }
                var imageUrls = await GetImageUrls();
                _defaultGameDiskProvider.Populate(imageUrls).Forget();
                cachedDisks = _defaultGameDiskProvider.GetDisks();
            }

        }
        protected abstract UniTask<List<string>> GetImageUrls();

        public virtual void Dispose()
        {
            _diskProvidersConfiguration.DefaultGameSourceChanged -= DefaultGameSourceChanged;

        }
    }

    public interface ICacheableDisks
    {
        
    }
}
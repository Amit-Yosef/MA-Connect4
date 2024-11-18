using System;
using System.Threading;
using Domains.DiskSources.Controllers;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Domains.DiskSources.Providers
{
    public abstract class BaseDiskProvider : IDisposable
    {
        [Inject] protected DynamicDisk.Factory DiskFactory;

        protected Transform Parent;

        protected CancellationTokenSource _cts = new CancellationTokenSource();

        public bool IsDataLoaded => disksCount > 1;
        protected abstract int disksCount { get; }
        

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
        protected void CreateParentIfMissing()
        {
            if (Parent == null)
            {
                
                Parent = new GameObject("TempDynamicDisksParent").transform;
                Parent.gameObject.SetActive(false);
                Object.DontDestroyOnLoad(Parent.gameObject);
            }
        }

        public virtual void Reset()
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = new CancellationTokenSource();
        }
    }
}
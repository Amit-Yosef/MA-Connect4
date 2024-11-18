using System;
using System.Threading;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Domains.DiskSources.Providers
{
    public abstract class BaseDiskProvider : IDisposable
    {
        protected Transform _parent;

        protected CancellationTokenSource _cts = new CancellationTokenSource();

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
        protected void CreateParentIfMissing()
        {
            if (_parent == null)
            {
                _parent = new GameObject(nameof(FixtureDiskProvider) + "_Parent").transform;
                Object.DontDestroyOnLoad(_parent.gameObject);
            }
        }
    }
}
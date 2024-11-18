using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using Domains.DiskSources;
using Domains.DiskSources.Controllers;
using Domains.DiskSources.Interfaces;
using MoonActive.Connect4;
using UnityEngine;
using Utils;
using Zenject;
using Object = UnityEngine.Object;

namespace Domains.DiskSources.Providers
{
    public class DefaultGameDiskProvider : BaseDiskProvider
    {
        private List<DiskData> _disks = new();

        protected override int disksCount
        {
            get => _disks.Count;
        }

        public async UniTask Populate(List<string> imageUrls)
        {
            _disks = new List<DiskData>();

            foreach (var imageUrl in imageUrls)
            {
                _cts.Token.ThrowIfCancellationRequested();
                var diskData = await GetDisk(imageUrl);
                if (diskData != null)
                {
                    _disks.Add(diskData);
                }
            }
        }

        private async UniTask<DiskData> GetDisk(string imageUrl)
        {
            CreateParentIfMissing();
            var logoSprite = await UrlImageUtils.LoadImageFromUrlAsync(imageUrl, _cts.Token);
            _cts.Token.ThrowIfCancellationRequested();
            if (logoSprite == null)
            {
                Debug.LogWarning($"Failed to load image from URL: {imageUrl}");
                return null;
            }

            var dynamicDisk = DiskFactory.Create(logoSprite);
            dynamicDisk.transform.SetParent(Parent);

            return new DiskData(dynamicDisk.Disk, logoSprite);
        }

        public List<DiskData> GetDisks()
        {
            return _disks ?? new List<DiskData>();
        }

        public void Populate(List<DiskData> disks)
        {
            _disks = disks;
        }

        public override void Reset()
        {
            base.Reset();
            _disks = new List<DiskData>();
        }

        public object Populate(List<DynamicDisk> cachedDisks)
        {
            throw new NotImplementedException();
        }
    }
}
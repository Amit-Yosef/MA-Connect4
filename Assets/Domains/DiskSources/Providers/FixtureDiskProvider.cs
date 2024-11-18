using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Data;
using Domains.DiskSources;
using Domains.DiskSources.Controllers;
using Domains.DiskSources.Data;
using Domains.DiskSources.Interfaces;
using MoonActive.Connect4;
using UnityEngine;
using UnityEngine.Networking;
using Utils;
using Zenject;
using Object = UnityEngine.Object;

namespace Domains.DiskSources.Providers
{
    public class FixtureDiskProvider : BaseDiskProvider
    {
        [Inject] private DynamicDisk.Factory _factory;

        private Dictionary<Fixture, List<DiskData>> _disks;
        public bool IsDataLoaded => _disks.Count > 0;

        public async UniTask Populate(List<Fixture> matches)
        {
            _disks = new();
            foreach (var match in matches)
            {
                await GetDisks(match, _cts.Token);
            }
        }

        public async UniTask<List<DiskData>> GetDisks(Fixture fixture, CancellationToken cancellationToken)
        {
            CreateParentIfMissing();

            if (_disks.TryGetValue(fixture, out var disks))
            {
                return disks;
            }

            var homeDisk = await GetDisk(fixture.Home, cancellationToken);
            var awayDisk = await GetDisk(fixture.Away, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            disks = new List<DiskData> { homeDisk, awayDisk };
            _disks[fixture] = disks;

            return disks;
        }

        private async UniTask<DiskData> GetDisk(FixtureMember fixtureMember, CancellationToken cancellationToken)
        {
            var logoSprite =
                await UrlImageUtils.LoadImageFromUrlAsync(fixtureMember.LogoUrl, TextureWrapMode.Clamp,
                    cancellationToken);
            var dynamicDisk = _factory.Create(logoSprite);
            dynamicDisk.transform.SetParent(_parent);

            return new DiskData(dynamicDisk.Disk, logoSprite);
        }

        public List<Fixture> GetFixtures() => _disks.Keys.ToList();

        public void Reset()
        {
            _disks = null;
        }
    }
}
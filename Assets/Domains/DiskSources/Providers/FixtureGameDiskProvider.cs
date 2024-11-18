using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using Domains.DiskSources.Data;
using Utils;
using Object = UnityEngine.Object;


namespace Domains.DiskSources.Providers
{
    public class FixtureGameDiskProvider : BaseDiskProvider
    {

        private Dictionary<Fixture, List<DiskData>> _disks = new ();
        protected override int disksCount { get => _disks.Count; }

        public async UniTask Populate(List<Fixture> matches)
        {
            _disks = new();
            foreach (var match in matches)
            {
                await CreateDisks(match);
            }
        }

        public async UniTask<List<DiskData>> CreateDisks(Fixture fixture)
        {
            CreateParentIfMissing();

            if (_disks.TryGetValue(fixture, out var disks))
            {
                return disks;
            }

            var homeDisk = await GetDisk(fixture.Home);
            var awayDisk = await GetDisk(fixture.Away);
            _cts.Token.ThrowIfCancellationRequested();
            disks = new List<DiskData> { homeDisk, awayDisk };
            _disks[fixture] = disks;

            return disks;
        }

        private async UniTask<DiskData> GetDisk(FixtureMember fixtureMember)
        {
            var logoSprite =
                await UrlImageUtils.LoadImageFromUrlAsync(fixtureMember.LogoUrl, _cts.Token);
            var dynamicDisk = DiskFactory.Create(logoSprite);
            
            dynamicDisk.transform.SetParent(Parent);

            var diskData =  new DiskData(dynamicDisk.Disk, logoSprite);
            return diskData;
        }

        public List<Fixture> GetFixtures() => _disks.Keys.ToList();

        public override void Reset()
        {
            base.Reset();
            _disks = new();
        }

        public Dictionary<Fixture, List<DiskData>> GetFixturesDisksDictionary() => _disks;

        public void Populate(Dictionary<Fixture, List<DiskData>> disks)
        {
            _disks = disks;
        }
    }
}
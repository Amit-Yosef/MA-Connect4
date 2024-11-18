using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Data;
using Data.FootballApi;
using MoonActive.Connect4;
using UnityEngine;
using UnityEngine.Networking;
using Utils;
using Zenject;
using Object = UnityEngine.Object;

namespace Controllers
{
    public class FootballDiskProvider : IDisposable
    {
        [Inject] private DynamicDisk.Factory _factory;
        
        private Transform _parent;
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly Dictionary<Match, List<DiskData>> _disks = new();

        public async UniTask Populate(List<Match> matches)
        {
            foreach (var match in matches)
            {
                await GetDisks(match, _cts.Token);
            }
        }
        
        public async UniTask<List<DiskData>> GetDisks(Match match, CancellationToken cancellationToken)
        {
            CreateParentIfMissing();

            if (_disks.TryGetValue(match, out var disks))
            {
                return disks;
            }

            var homeDisk = await GetDisk(match.HomeTeam, cancellationToken);
            var awayDisk = await GetDisk(match.AwayTeam,cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            disks = new List<DiskData> { homeDisk, awayDisk };
            _disks[match] = disks;

            return disks;
        }

        private async UniTask<DiskData> GetDisk(Team team, CancellationToken cancellationToken)
        {
            var logoSprite = await UrlImageUtils.LoadImageFromUrlAsync(team.LogoUrl, TextureWrapMode.Clamp, cancellationToken);
            var dynamicDisk = _factory.Create(logoSprite);
            dynamicDisk.transform.SetParent(_parent);

            return new DiskData(dynamicDisk.Disk, logoSprite);
        }
        
        public List<Match> GetFixtures() => _disks.Keys.ToList();
        
        private void CreateParentIfMissing()
        {
            if (_parent == null)
            {
                _parent = new GameObject(nameof(FootballDiskProvider) + "_Parent").transform;
                Object.DontDestroyOnLoad(_parent.gameObject);
            }
        }

        public bool IsDataLoaded() => _disks.Count > 0;

        public void Dispose()
        {
           _cts.Cancel();
           _cts.Dispose();
        }
    }



}
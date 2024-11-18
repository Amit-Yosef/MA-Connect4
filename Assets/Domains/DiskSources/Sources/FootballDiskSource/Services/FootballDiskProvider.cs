using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Data;
using Data.FootballApi;
using Domains.DiskSources.Interfaces;
using MoonActive.Connect4;
using UnityEngine;
using UnityEngine.Networking;
using Utils;
using Zenject;

namespace Controllers
{
    public class FootballDiskProvider : IFixtureDiskProvider<Match>
    {
        [Inject] private DynamicDisk.Factory _factory;
        
        private Transform _parent;
        private readonly Dictionary<Match, List<DiskData>> _disks = new();

        public async UniTask Populate(List<Match> matches)
        {
            foreach (var match in matches)
            {
                await GetDisks(match);
            }
        }
        
        public async UniTask<List<DiskData>> GetDisks(Match match)
        {
            CreateParentIfMissing();

            if (_disks.TryGetValue(match, out var disks))
            {
                return disks;
            }

            var homeDisk = await GetDisk(match.HomeTeam);
            var awayDisk = await GetDisk(match.AwayTeam);

            disks = new List<DiskData> { homeDisk, awayDisk };
            _disks[match] = disks;

            return disks;
        }

        private async UniTask<DiskData> GetDisk(Team team)
        {
            var logoSprite = await UrlImageUtils.LoadImageFromUrlAsync(team.LogoUrl, TextureWrapMode.Clamp);
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
    }



}
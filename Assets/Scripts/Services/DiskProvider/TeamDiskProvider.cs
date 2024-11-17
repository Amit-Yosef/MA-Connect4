using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Data;
using Data.FootballApi;
using MoonActive.Connect4;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Controllers
{
    public class TeamDiskProvider 
    {
        [Inject] private DynamicDisk.Factory _factory;
        
        private Transform _parent;
        private readonly Dictionary<Team, DiskData> _disks = new Dictionary<Team, DiskData>();

                
        public void CreateParentIfMissing()
        {
            if (_parent == null)
            {
                _parent = new GameObject(nameof(TeamDiskProvider) + "_Parent").transform;
                Object.DontDestroyOnLoad(_parent.gameObject);
            }
        }

        public async UniTask<List<DiskData>> GetDisks(Match match)
        {
            CreateParentIfMissing();
            
            List<DiskData> disks = new List<DiskData>(); 
            disks.Add(await GetDisk(match.HomeTeam));
            disks.Add(await GetDisk(match.AwayTeam));
            return disks;
        }

        private async UniTask<DiskData> GetDisk(Team team)
        {
            if (_disks.ContainsKey(team))
            {
                return _disks[team];
            }
            var logoSprite = await LoadImageFromUrlAsync(team.LogoUrl);
            var dynamicDisk = _factory.Create(logoSprite);
            dynamicDisk.transform.SetParent(_parent);


            DiskData diskData = new DiskData(dynamicDisk.Disk, logoSprite);
            
            _disks[team] = diskData;
            return diskData;
        }

        private async Task<Sprite> LoadImageFromUrlAsync(string url)
        {
            using var request = UnityWebRequestTexture.GetTexture(url);
            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to load image: {request.error}");
                return null;
            }

            var texture = DownloadHandlerTexture.GetContent(request);
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
        }

    }

}
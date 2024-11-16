using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Data.FootballApi;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Controllers
{
    public class TeamDiskProvider
    {
        [Inject] private DynamicDisk.Factory _factory;
        private readonly Dictionary<Team, DynamicDisk> _teamPrefabs = new Dictionary<Team, DynamicDisk>();

        public async Task CreateTeamPrefabAsync(Team team)
        {
            if (_teamPrefabs.ContainsKey(team))
            {
                Debug.LogWarning($"Prefab for team {team.Name} already exists.");
                return;
            }
            var logoSprite = await LoadImageFromUrlAsync(team.LogoUrl);

            var diskObject = _factory.Create(team, logoSprite);


            _teamPrefabs[team] = diskObject;
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
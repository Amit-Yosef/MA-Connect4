using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Project.Utils
{
    public static class UrlImageUtils
    {
        public static async UniTask<Sprite> LoadImageFromUrlAsync(string url, CancellationToken cancellationToken, TextureWrapMode mode = TextureWrapMode.Clamp)
        {
            using var request = UnityWebRequestTexture.GetTexture(url);
            await request.SendWebRequest().ToUniTask(cancellationToken: cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to load image: {request.error}");
                return null;
            }

            var texture = DownloadHandlerTexture.GetContent(request);
            OptimizeTextureSettings(texture, mode);
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f, 100f);
        }

        private static void OptimizeTextureSettings(Texture2D texture, TextureWrapMode mode)
        {
            texture.filterMode = FilterMode.Trilinear;

            if (texture.mipmapCount > 1)
            {
                texture.Apply(true, false);
            }

            texture.anisoLevel = 9;
            texture.wrapMode = mode;
        }
    }
}
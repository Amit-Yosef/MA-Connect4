using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Domains.DiskSources.Sources.Foodish
{
    public class FoodishClient : IDisposable
    {
        private const string ApiUrl = "https://foodish-api.com/api/";
        private readonly HttpClient _httpClient;

        public FoodishClient()
        {
            _httpClient = new HttpClient();
        }

        public async UniTask<List<string>> GetRandomFoodImageUrls(int count, CancellationToken cancellationToken)
        {
            var imageUrls = new List<string>();

            for (int i = 0; i < count; i++)
            {
                try
                {
                    string jsonResponse = await _httpClient.GetStringAsync(ApiUrl);
                    cancellationToken.ThrowIfCancellationRequested();

                    string imageUrl = ExtractImageUrl(jsonResponse);
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        imageUrls.Add(imageUrl);
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Failed to fetch image URL: {ex.Message}");
                }
            }

            return imageUrls;
        }

        private string ExtractImageUrl(string jsonResponse)
        {
            try
            {
                var jsonObject = JsonUtility.FromJson<FoodishResponse>(jsonResponse);
                return jsonObject.image;
            }
            catch (Exception ex)
            {
                Debug.LogError($"JSON parsing error: {ex.Message}");
                return null;
            }
        }

        [Serializable]
        private class FoodishResponse
        {
            public string image;
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
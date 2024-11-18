using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using Domains.DiskSources.Controllers;
using Domains.DiskSources.Interfaces;
using Domains.DiskSources.Sources.Foodish;
using Zenject;

namespace Domains.DiskSources.Sources.FakePeople
{
    public class FoodishGameDiskSource : DefaultGameWebDiskSource
    {
        [Inject] private DynamicDisk.Factory _diskFactory;
        private FoodishClient _client = new();
        private CancellationTokenSource _cts = new();
        
        protected override DefaultGameDiskSourceType GetDefaultDiskSource() => DefaultGameDiskSourceType.Food; 
        

        protected override async UniTask<List<string>> GetImageUrls()
        {
            return  await _client.GetRandomFoodImageUrls(10,_cts.Token);

        }

        public override void Dispose()
        {
            base.Dispose();
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}
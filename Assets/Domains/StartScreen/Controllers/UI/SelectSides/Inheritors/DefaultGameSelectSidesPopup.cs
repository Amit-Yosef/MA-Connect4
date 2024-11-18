using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;
using Domains.DiskSources.Providers;
using Zenject;

namespace Controllers.UI.StartScreen.SelectSides
{
    public class DefaultGameSelectSidesPopup : SelectSidesPopup
    {
        [Inject] private DefaultGameDiskProvider _gameDiskProvider;
        
        [Inject]
        public override async UniTaskVoid Construct()
        {
            await WaitForDataToLoad(_gameDiskProvider, _cts.Token);
            playersView.Set(_gameDiskProvider.GetDisks(), turnStrategyService.GetAllStrategies());
        }
        
    }
}
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;
using Domains.DiskSources.Providers;
using Zenject;

namespace Controllers.UI.StartScreen.SelectSides
{
    public class NormalSelectSidesPopup : SelectSidesPopup
    {
        [Inject] private DefaultDiskProvider _diskProvider;
        [Inject]
        public override async UniTaskVoid Construct()
        {
            playersView.Set(_diskProvider.GetDisks(), turnStrategyService.GetAllStrategies());
        }
        
    }
}
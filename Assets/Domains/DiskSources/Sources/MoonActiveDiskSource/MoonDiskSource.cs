using System;
using System.Collections.Generic;
using Controllers;
using Cysharp.Threading.Tasks;
using Data;
using Domains.DiskSources.Providers;
using UnityEngine;
using Zenject;

namespace Domains.DiskSources.Sources.Football
{
    public class MoonDiskSource  : IInitializable
    {
        
        [Inject] private DiskProvidersConfiguration _diskProvidersConfiguration;
        [Inject] private DefaultDiskProvider _diskProvider;
        [Inject] private List<DiskData> _diskDatas;

        
        public void Initialize()
        {
            _diskProvidersConfiguration.DefaultGameModeProviderChanged += DefaultGameModeProviderChanged;
        }

        private void DefaultGameModeProviderChanged(DefaultGameModeDiskSource mode)
        {
            if (mode== DefaultGameModeDiskSource.MoonActive)
            {
                _diskProvider.Reset();
                _diskProvider.Populate(_diskDatas);
            }

        }
    }
}
using System;
using System.Collections.Generic;
using Controllers;
using Cysharp.Threading.Tasks;
using Data;
using Domains.DiskSources;
using Domains.DiskSources.Data;
using Domains.DiskSources.Providers;
using UnityEngine;
using Zenject;

namespace Domains.DiskSources.Sources.Football
{
    public class FootballDiskSource  : IInitializable
    {
        [Inject] private DiskProvidersConfiguration _disProvidersConfiguration;
         private FootballApiClient _footballApiClient = new FootballApiClient();
         [Inject] private FixtureDiskProvider _diskProvider;

        public List<Fixture> Matches { get; private set; }

        
        public void Initialize()
        {
            _disProvidersConfiguration.FixtureGameModeProviderChanged += DefaultGameModeProviderChanged;
        }

        private async void DefaultGameModeProviderChanged(FixtureGameModeDiskSource mode)
        {
            if (mode== FixtureGameModeDiskSource.Football)
            {
                _diskProvider.Reset();
                await FetchMatchesAsync();
                await _diskProvider.Populate(Matches);
            }

        }
        
        private async UniTask FetchMatchesAsync()
        {
            Matches = await UniTask.RunOnThreadPool(() =>
                _footballApiClient.GetNextMatchesByLeagueIdAsync((int)FootballConsts.League));
        }
    }
}
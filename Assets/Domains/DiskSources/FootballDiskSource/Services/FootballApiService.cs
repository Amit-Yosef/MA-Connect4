using System;
using System.Collections.Generic;
using Controllers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Data.FootballApi
{
    public class FootballApiService  : IInitializable
    {
        [Inject] private DiskDataSourceConfig _diskDataSourceConfig;
        [Inject] private FootballApiClient _footballApiClient;
        [Inject] private FootballDiskProvider _footballDiskProvider;

        public List<Match> Matches { get; private set; }

        private FootballLeague _league = FootballLeague.IsraeliLeague;
        
        public void Initialize()
        {
            _diskDataSourceConfig.OnPlayersConfigurationModeChanged += OnPlayersConfigurationModeChanged;
        }

        private async void OnPlayersConfigurationModeChanged(DiskDataSource mode)
        {
            if (mode== DiskDataSource.Football)
            {
                await FetchMatchesAsync();
                await _footballDiskProvider.PopulateDisks(Matches);
            }

        }
        
        private async UniTask FetchMatchesAsync()
        {
            Matches = await UniTask.RunOnThreadPool(() =>
                _footballApiClient.GetNextMatchesByLeagueIdAsync((int)_league));
        }

        
    }
}
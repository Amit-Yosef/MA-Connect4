using System;
using System.Collections.Generic;
using Controllers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using IInitializable = Unity.VisualScripting.IInitializable;

namespace Data.FootballApi
{
    public class FootballApiService 
    {
        [Inject] private AppConfiguration _appConfiguration;
        [Inject] private FootballApiFetcher _footballApiFetcher;
        [Inject] private FootballDiskProvider _footballDiskProvider;

        public List<Match> Matches { get; private set; }

        private FootballLeague _league = FootballLeague.IsraeliLeague;

        [Inject]
        public void Construct()
        {
            _appConfiguration.OnPlayersConfigurationModeChanged += OnPlayersConfigurationModeChanged;
        }

        private async void OnPlayersConfigurationModeChanged(DiskProviderMode mode)
        {
            if (mode== DiskProviderMode.Football)
            {
                await FetchMatchesAsync();
                await _footballDiskProvider.PopulateDisks(Matches);
            }

        }
        
        private async UniTask FetchMatchesAsync()
        {
            Matches = await UniTask.RunOnThreadPool(() =>
                _footballApiFetcher.GetNextMatchesByLeagueIdAsync((int)_league));
        }
    }
}
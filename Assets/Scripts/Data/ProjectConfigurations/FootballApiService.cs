using System;
using System.Collections.Generic;
using Controllers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using IInitializable = Unity.VisualScripting.IInitializable;

namespace Data.FootballApi
{
    public class FootballApiService : IInitializable
    {
        [Inject] private AppConfiguration _appConfiguration;
        [Inject] private FootballApiFetcher _footballApiFetcher;
        public List<Match> Matches { get; private set; }
        public bool IsDataLoaded { get; private set; }
        
        [Inject]
        public void Initialize()
        {
            IsDataLoaded = false;
            _appConfiguration.OnPlayersConfigurationModeChanged += OnPlayersConfigurationModeChanged;
            Debug.Log("Subscribing to OnPlayersConfigurationModeChanged");
        }

        private void OnPlayersConfigurationModeChanged(PlayersConfigurationMode mode)
        {
            if (!IsDataLoaded && mode == PlayersConfigurationMode.Football)
            {
                FetchMatchesAsync().Forget();
            }
        }

        private async UniTask FetchMatchesAsync()
        {
            IsDataLoaded = false;
            Matches = await UniTask.RunOnThreadPool(() => _footballApiFetcher.GetNextMatchesByLeagueIdAsync(39));
            IsDataLoaded = true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using Domains.DiskSources;
using Domains.DiskSources.Data;
using Domains.DiskSources.Providers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Controllers.UI.StartScreen.SelectSides
{
    public class FixtureGameSelectSidePopup : SelectSidesPopup
    {
        [SerializeField] private FixtureView fixtureView;

        [Inject] private FixtureGameDiskProvider _gameDiskProvider;

        private int _currentFixtureIndex;
        private List<Fixture> _fixtures => _gameDiskProvider.GetFixtures();

        [Inject]
        public override async UniTaskVoid Construct()
        {
            await WaitForDataToLoad(_gameDiskProvider, _cts.Token);
            await SetPlayerViewByIndex(_currentFixtureIndex);
            await fixtureView.SetBackgroundImage(_fixtures.First(), _cts.Token);

        }

        private async UniTask SetPlayerViewByIndex(int index)
        {
            var diskOptions = await _gameDiskProvider.CreateDisks(_fixtures[index]);
            var strategyOptions = turnStrategyService.GetAllStrategies() ;
            playersView.Set(diskOptions,strategyOptions);
            _currentFixtureIndex = index;
        }
        

        public async void Next()
        {
            if (_fixtures.Count == 0 || _cts.Token.IsCancellationRequested) return;

            int nextIndex = (_currentFixtureIndex + 1) % _fixtures.Count;
            await SetPlayerViewByIndex(nextIndex);
            fixtureView.Set(_fixtures[nextIndex]);
        }
        
    }
}
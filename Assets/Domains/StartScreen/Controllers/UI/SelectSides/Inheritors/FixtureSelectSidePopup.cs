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
    public class FixtureSelectSidePopup : SelectSidesPopup
    {
        [SerializeField] private FixtureView fixtureView;
        [SerializeField] private CanvasGroup loadingIndicator;

        [Inject] private FixtureDiskProvider _diskProvider;

        private int _currentFixtureIndex;
        private List<Fixture> _fixtures => _diskProvider.GetFixtures();

        
        public override async UniTaskVoid Construct()
        {
            await WaitForDataToLoad(_cts.Token);
            await SetPlayerViewByIndex(_currentFixtureIndex);
            await fixtureView.SetBackgroundImage(_fixtures.First(), _cts.Token);

        }

        private async UniTask SetPlayerViewByIndex(int index)
        {
            var diskOptions = await _diskProvider.GetDisks(_fixtures[index], _cts.Token);
            var strategyOptions = turnStrategyService.GetAllStrategies() ;
            playersView.Set(diskOptions,strategyOptions);
            _currentFixtureIndex = index;
        }
        private async UniTask WaitForDataToLoad(CancellationToken cancellationToken)
        {
            playersView.canvasGroup.interactable = false;
            playersView.canvasGroup.alpha = 0;
            loadingIndicator.alpha = 1;
            await UniTask.WaitUntil(() => _diskProvider.IsDataLoaded, cancellationToken: cancellationToken);
            LeanTween.alphaCanvas(loadingIndicator, 0, 0.2f)
                .setOnComplete(() => loadingIndicator.gameObject.SetActive(false));
            playersView.canvasGroup.interactable = true;
            playersView.canvasGroup.alpha = 1;
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
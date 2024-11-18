using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using Domains.DiskSources.Providers;
using Managers;
using UnityEngine;
using Zenject;

namespace Controllers.UI.StartScreen.SelectSides
{
    public abstract class SelectSidesPopup : PopupController
    {
        [Inject] protected PlayerTurnStrategyService turnStrategyService;
        [Inject] private SceneSwitchingSystem _sceneSwitchingSystem;
        
        [SerializeField] private CanvasGroup loadingIndicator;

        [SerializeField] protected PlayersView playersView;

        protected CancellationTokenSource _cts = new CancellationTokenSource();
        
        protected async UniTask WaitForDataToLoad(BaseDiskProvider provider, CancellationToken cancellationToken)
        {
            playersView.canvasGroup.interactable = false;
            playersView.canvasGroup.alpha = 0;
            loadingIndicator.alpha = 1;
            await UniTask.WaitUntil(() => provider.IsDataLoaded, cancellationToken: cancellationToken);
            LeanTween.alphaCanvas(loadingIndicator, 0, 0.2f)
                .setOnComplete(() => loadingIndicator.gameObject.SetActive(false));
            playersView.canvasGroup.interactable = true;
            playersView.canvasGroup.alpha = 1;
        }

        public virtual void Submit()
        {
            playersView.UpdatePlayersConfig();
            Close();
            _sceneSwitchingSystem.LoadSceneAsync(SceneID.GameScene).Forget();
        }

        public abstract UniTaskVoid Construct();
        
        private void OnDestroy()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
}
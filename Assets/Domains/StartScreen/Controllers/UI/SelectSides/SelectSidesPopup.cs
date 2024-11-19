using System.Threading;
using Cysharp.Threading.Tasks;
using Domains.StartScreen.Services;
using Managers;
using UnityEngine;
using Zenject;

namespace Controllers.UI.StartScreen.SelectSides
{
    public class SelectSidesPopup : PopupController
    {
        [Inject] protected PlayerTurnStrategyService turnStrategyService;
        [Inject] private SceneSwitchingSystem _sceneSwitchingSystem;
        
        [SerializeField] private CanvasGroup loadingIndicator;

        [SerializeField] protected PlayersView playersView;

        protected CancellationTokenSource _cts = new CancellationTokenSource();
        
        [Inject] private DiskProvider _diskProvider;
        
        [Inject]
        public async UniTaskVoid Construct()
        {
            await WaitForDataToLoad(_cts.Token);
            playersView.Set(_diskProvider.GetDisks(), turnStrategyService.GetAllStrategies());
        }
        protected async UniTask WaitForDataToLoad(CancellationToken cancellationToken)
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

        public virtual void Submit()
        {
            playersView.UpdatePlayersConfig();
            Close();
            _sceneSwitchingSystem.LoadSceneAsync(SceneID.GameScene).Forget();
        }

        private void OnDestroy()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
}
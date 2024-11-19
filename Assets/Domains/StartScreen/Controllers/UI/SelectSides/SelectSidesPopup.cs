using System.Threading;
using Cysharp.Threading.Tasks;
using Domains.StartScreen.Services;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Controllers.UI.StartScreen.SelectSides
{
    public class SelectSidesPopup : PopupController
    {
        [Inject] protected PlayerTurnStrategyService turnStrategyService;
        [Inject] private SceneSwitchingSystem _sceneSwitchingSystem;
        [Inject] private DiskProvider _diskProvider;

        [SerializeField] private CanvasGroup loadingIndicator;
        [SerializeField] private CanvasGroup backgroundCanvasGroup;
        [SerializeField] protected PlayersView playersView;
        
        private CancellationTokenSource _cts = new CancellationTokenSource();

        [Inject]
        public async UniTaskVoid Construct()
        {
            await WaitForDataToLoad(_cts.Token);
            playersView.Set(_diskProvider.GetDisks(), turnStrategyService.GetAllStrategies());
        }

        private async UniTask WaitForDataToLoad(CancellationToken cancellationToken)
        {
            backgroundCanvasGroup.alpha = 0;
            playersView.canvasGroup.interactable = false;
            playersView.canvasGroup.alpha = 0;
            loadingIndicator.alpha = 1;
            await UniTask.WaitUntil(() => _diskProvider.IsDataLoaded, cancellationToken: cancellationToken);
            LeanTween.alphaCanvas(loadingIndicator, 0, 0.2f)
                .setOnComplete(() => loadingIndicator.gameObject.SetActive(false));
            LeanTween.alphaCanvas(backgroundCanvasGroup, 1, 1f);
            playersView.canvasGroup.interactable = true;
            playersView.canvasGroup.alpha = 1;
        }

        public virtual void Submit()
        {
            playersView.UpdatePlayersConfig();
            LeanTween.rotateZ(backgroundCanvasGroup.gameObject, 360f, 1f).setEase(LeanTweenType.easeShake).setLoopOnce()
                .setOnComplete(() =>
                {
                    _sceneSwitchingSystem.LoadSceneAsync(SceneID.GameScene).Forget();
                    Close();
                });

        }

        private void OnDestroy()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
}
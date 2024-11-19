using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Systems;
using StartScreen.Controllers.UI.SelectSides.InnerControllers;
using StartScreen.DataProviders;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Zenject;

namespace StartScreen.Controllers.UI.SelectSides
{
    public class SelectSidesPopup : PopupController
    {
        [Inject] protected PlayerBehavioursProvider BehavioursProvider;
        [Inject] private SceneSwitchingSystem _sceneSwitchingSystem;
        [Inject] private DiskDataProvider _diskDataProvider;

        [SerializeField] private CanvasGroup loadingIndicator;
        [SerializeField] private CanvasGroup sameDisksErrorCanvasGroup;
        [SerializeField] private CanvasGroup backgroundCanvasGroup;
        [SerializeField] protected PlayersView playersView;
        [SerializeField] private CanvasGroup submitButtonCanvasGroup;

        private CancellationTokenSource _cts = new CancellationTokenSource();
        private bool _isSubmitting;

        [Inject]
        public async UniTaskVoid Construct()
        {
            await WaitForDataToLoad(_cts.Token);
            playersView.Set(_diskDataProvider.GetDisks(), BehavioursProvider.GetAllStrategies());
            playersView.InvalidDisksSelection += OnInvalidDisksSelection;
        }

        private void OnInvalidDisksSelection()
        {

            submitButtonCanvasGroup.interactable = false;
            LeanTween.cancel(sameDisksErrorCanvasGroup.gameObject);
            LeanTween.alphaCanvas(sameDisksErrorCanvasGroup, 1, 0.5f);
            playersView.ValidDiskSelection += ValidDiskSelection;

            void ValidDiskSelection()
            {
                submitButtonCanvasGroup.interactable = true;
                LeanTween.cancel(sameDisksErrorCanvasGroup.gameObject);
                LeanTween.alphaCanvas(sameDisksErrorCanvasGroup, 0, 0.5f);
                playersView.ValidDiskSelection -= ValidDiskSelection;
            }
        }

        private async UniTask WaitForDataToLoad(CancellationToken cancellationToken)
        {
            backgroundCanvasGroup.alpha = 0;
            playersView.canvasGroup.interactable = false;
            playersView.canvasGroup.alpha = 0;
            loadingIndicator.alpha = 1;
            await UniTask.WaitUntil(() => _diskDataProvider.IsDataLoaded, cancellationToken: cancellationToken);
            LeanTween.alphaCanvas(loadingIndicator, 0, 0.2f)
                .setOnComplete(() => loadingIndicator.gameObject.SetActive(false));
            LeanTween.alphaCanvas(backgroundCanvasGroup, 1, 1f);
            playersView.canvasGroup.interactable = true;
            playersView.canvasGroup.alpha = 1;
        }

        public void Submit()
        {
            if (_isSubmitting)
                return;

            _isSubmitting = true;
            playersView.UpdatePlayersConfig();
            LeanTween.rotateZ(backgroundCanvasGroup.gameObject, 360f, 1f)
                .setEase(LeanTweenType.easeShake)
                .setLoopOnce()
                .setOnComplete(() =>
                {
                    _sceneSwitchingSystem.LoadSceneAsync(SceneID.GameScene).Forget();
                    Close();
                    _isSubmitting = false;
                });
        }
        private void OnDestroy()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
}
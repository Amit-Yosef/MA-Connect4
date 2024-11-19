using AYellowpaper.SerializedCollections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Systems
{
    public class SceneSwitchingSystem : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _fadeCanvasGroup;
        [SerializeField] private float _fadeDuration = 3f;

        [SerializedDictionary("scene id", "scene name")] [SerializeField]
        private SerializedDictionary<SceneID, string> scenes;

        public async UniTask LoadSceneAsync(SceneID sceneID)
        {
            await FadeOutAsync();

            await SceneManager.LoadSceneAsync(scenes[sceneID]);

            await FadeInAsync();
        }

        private async UniTask FadeOutAsync()
        {
            _fadeCanvasGroup.blocksRaycasts = true;
            await FadeCanvasGroupAsync(_fadeCanvasGroup, 1f, _fadeDuration);
        }

        private async UniTask FadeInAsync()
        {
            await FadeCanvasGroupAsync(_fadeCanvasGroup, 0f, _fadeDuration);
            _fadeCanvasGroup.blocksRaycasts = false;
        }

        private UniTask FadeCanvasGroupAsync(CanvasGroup canvasGroup, float targetAlpha, float duration)
        {
            var tcs = new UniTaskCompletionSource();
            LeanTween.alphaCanvas(canvasGroup, targetAlpha, duration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setOnComplete(() => tcs.TrySetResult());
            return tcs.Task;
        }
    }

    public enum SceneID
    {
        GameScene,
        StartScene
    }
}
using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Zenject;

namespace Project.Systems
{
    public class SceneSwitchingController : MonoBehaviour
    {
        
        [SerializeField] private CanvasGroup fadeCanvasGroup;
        [SerializeField] private float fadeDuration = 3f;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public async UniTask FadeOutAsync()
        {
            fadeCanvasGroup.blocksRaycasts = true;
            await FadeCanvasGroupAsync(fadeCanvasGroup, 1f, fadeDuration);
        }

        public async UniTask FadeInAsync()
        {
            await FadeCanvasGroupAsync(fadeCanvasGroup, 0f, fadeDuration);
            fadeCanvasGroup.blocksRaycasts = false;
        }

        private UniTask FadeCanvasGroupAsync(CanvasGroup canvasGroup, float targetAlpha, float duration)
        {
            var tcs = new UniTaskCompletionSource();
            LeanTween.alphaCanvas(canvasGroup, targetAlpha, duration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setOnComplete(() => tcs.TrySetResult());
            return tcs.Task;
        }
        
        public class Factory : PlaceholderFactory<SceneSwitchingController>
        {
        }
    }

    public enum SceneID
    {
        GameScene,
        StartScene
    }
}
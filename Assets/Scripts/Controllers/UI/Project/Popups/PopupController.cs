using System;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class PopupController : MonoBehaviour
{
    private const float StartPosition = -2000;

    [Header("Animation Settings")] [SerializeField]
    private float tweenDuration = 0.3f;

    [SerializeField] private LeanTweenType tweenType = LeanTweenType.easeSpring;

    [FormerlySerializedAs("rectTransform")] [Space] [SerializeField]
    private RectTransform body;

    [SerializeField] private CanvasGroup background;

    private void Awake()
    {
        if (body == null)
        {
            Debug.LogError($"RectTransform is not assigned in {nameof(PopupController)} on {gameObject.name}");
            enabled = false;
        }
    }

    public void Open(Vector2 position)
    {
        if (body == null) return;

        body.anchoredPosition = new Vector2(position.x, StartPosition);
        gameObject.SetActive(true);

        LeanTween.value(StartPosition, position.y, tweenDuration)
            .setEase(tweenType)
            .setOnUpdate(y => body.anchoredPosition = new Vector2(position.x, y));

        if (background != null)
        {
            background.alpha = 0;
            LeanTween.alphaCanvas(background, 1, tweenDuration).setEase(tweenType);
        }
    }

    public void Close()
    {
        if (body == null) return;

        if (background != null)
        {
            LeanTween.alphaCanvas(background, 0, tweenDuration).setEase(tweenType);
        }

        LeanTween.moveY(body, StartPosition, tweenDuration)
            .setEase(tweenType)
            .setOnComplete(() => { Destroy(gameObject); });
    }
}
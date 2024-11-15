using System;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class PopupController : MonoBehaviour
{
    private const float  StartPosition = -2000;

    [Header("Animation Settings")]
    [SerializeField] private float tweenDuration = 0.3f;
    [SerializeField] private LeanTweenType tweenType = LeanTweenType.easeSpring;
    [Space]
    [SerializeField] private RectTransform rectTransform;

    private void Awake()
    {
        if (rectTransform == null)
        {
            Debug.LogError($"RectTransform is not assigned in {nameof(PopupController)} on {gameObject.name}");
        }
    }
    

    public void Open(Vector2 position)
    {
        if (rectTransform == null) return;

        

        rectTransform.anchoredPosition = new Vector2(position.x, StartPosition);
        gameObject.SetActive(true);

        LeanTween.value(StartPosition, position.y, tweenDuration)
            .setEase(tweenType)
            .setOnUpdate(y => rectTransform.anchoredPosition = new Vector2(position.x, y));
    }

    public void Close()
    {
        if (rectTransform == null) return;

        LeanTween.moveY(rectTransform, StartPosition, tweenDuration)
            .setEase(tweenType)
            .setOnComplete(() => Destroy(gameObject));
    }
}
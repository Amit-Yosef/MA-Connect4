using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Domains.DiskSources;
using Domains.DiskSources.Data;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Zenject;

public class FixtureView : MonoBehaviour
{
    [SerializeField] private Image tiledBackGroundImage;
    [SerializeField] private CanvasGroup tiledBackGroundCanvasGroup;
    [SerializeField] private Text dateText;

    private int _currentMatchIndex;
    
    public void Set(Fixture fixture)
    {
        dateText.text = fixture.Date.ToShortDateString() +", "+ fixture.Date.ToShortTimeString();
    }
    
    public async UniTask SetBackgroundImage(Fixture fixture, CancellationToken cancellationToken)
    {
        tiledBackGroundCanvasGroup.alpha = 0;
        tiledBackGroundImage.sprite = await UrlImageUtils.LoadImageFromUrlAsync(fixture.BackgroundImage, cancellationToken, TextureWrapMode.Repeat);

        LeanTween.alphaCanvas(tiledBackGroundCanvasGroup, 1f, 1.5f).setEase(LeanTweenType.easeInOutQuad);
    }


}

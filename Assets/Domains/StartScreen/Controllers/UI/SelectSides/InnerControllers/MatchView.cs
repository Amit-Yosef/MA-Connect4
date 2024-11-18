using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data.FootballApi;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Zenject;

public class MatchView : MonoBehaviour
{
    [SerializeField] private Image tiledBackGroundImage;
    [SerializeField] private CanvasGroup tiledBackGroundCanvasGroup;
    [SerializeField] private Text dateText;

    private int _currentMatchIndex;
    
    public void Set(Match match)
    {
        dateText.text = match.Date.ToShortDateString() +", "+ match.Date.ToShortTimeString();
    }
    
    public async UniTask SetBackgroundImage(Match match, CancellationToken cancellationToken)
    {
        tiledBackGroundCanvasGroup.alpha = 0;
        tiledBackGroundImage.sprite = await UrlImageUtils.LoadImageFromUrlAsync(match.LeagueLogoUrl, TextureWrapMode.Repeat, cancellationToken);

        LeanTween.alphaCanvas(tiledBackGroundCanvasGroup, 1f, 1.5f).setEase(LeanTweenType.easeInOutQuad);
    }


}

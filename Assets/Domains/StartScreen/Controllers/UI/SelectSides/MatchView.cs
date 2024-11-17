using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Data.FootballApi;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Zenject;

public class MatchView : MonoBehaviour
{
    [Inject] private FootballApiService _footballApiService;
    [SerializeField] private Image tiledBackGroundImage;
    [SerializeField] private CanvasGroup tiledBackGroundCanvasGroup;
    [SerializeField] private Text dateText;

    private int _currentMatchIndex;
    private List<Match> _matches => _footballApiService.Matches;

    private void OnEnable()
    {
        SetBackgroundImage(_footballApiService.Matches.First()).Forget();
    }

    public void NextMatch()
    {
        _currentMatchIndex = (_currentMatchIndex + 1) % _matches.Count;
        var match = _matches[_currentMatchIndex];
        dateText.text = match.Date.ToShortDateString() +", "+ match.Date.ToShortTimeString();
    }
    
    private async UniTask SetBackgroundImage(Match match)
    {
        tiledBackGroundCanvasGroup.alpha = 0;
        tiledBackGroundImage.sprite = await UrlImageUtils.LoadImageFromUrlAsync(match.LeagueLogoUrl, TextureWrapMode.Repeat);

        LeanTween.alphaCanvas(tiledBackGroundCanvasGroup, 1f, 1.5f).setEase(LeanTweenType.easeInOutQuad);
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Controllers.Players;
using Game.Managers;
using UnityEngine;
using Zenject;

public class TurnManager : IInitializable, IDisposable
{
    [Inject] private PlayersManager _playersManager;
    [Inject] private GameManager _gameManager;

    private List<PlayerController> _players;
    private int _currentPlayerIndex;
    private PlayerController _currentPlayerController;
    private CancellationTokenSource _cts;

    public void Initialize()
    {
        _gameManager.OnGameOver += OnGameOver;
        _cts = new CancellationTokenSource();
        StartTurnLoop().Forget();

    }

    private void OnGameOver()
    {
        _cts.Cancel();
    }

    private async UniTaskVoid StartTurnLoop()
    {
        _players = _playersManager.Players;
        _currentPlayerController = _players.First();
        await PerformTurn();
    }
    
    private async UniTask PerformTurn()
    {
        if (_cts.Token.IsCancellationRequested)
            return;

        try
        {
            await _currentPlayerController.DoTurn(_cts.Token);
        }
        catch (Exception ex)
        {
            Debug.LogError($"An error occurred during the turn: {ex.Message}");
            return;
        }

        if (!_cts.Token.IsCancellationRequested)
        {
            AdvanceToNextPlayer();
            await PerformTurn();
        }
    }


    private void AdvanceToNextPlayer()
    {
        _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
        _currentPlayerController = _players[_currentPlayerIndex];
    }

    public void Dispose()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _gameManager.OnGameOver -= OnGameOver;
    }

   
}
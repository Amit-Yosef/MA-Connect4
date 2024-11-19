using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Controllers;
using Controllers.Players;
using Cysharp.Threading.Tasks;
using Data;
using Managers;
using MoonActive.Connect4;
using UnityEngine;
using Zenject;

public class TurnManager : IInitializable, IDisposable
{
    [Inject] private PlayersManager _playersManager;
    
    [Inject] private GameManager _gameManager;

    private List<Player> players;
    private int _currentPlayerIndex = 0;
    private Player _currentPlayer;
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
        players = _playersManager.Players;
        _currentPlayer = players.First();
        await PerformTurn();
    }
    
    private async UniTask PerformTurn()
    {
        if (_cts.Token.IsCancellationRequested)
            return;

        try
        {
            await _currentPlayer.DoTurn(_cts.Token);
        }
        catch (Exception ex)
        {
            Debug.LogError($"An error occurred during the turn: {ex.Message}");
        }

        if (!_cts.Token.IsCancellationRequested)
        {
            AdvanceToNextPlayer();
            await PerformTurn();
        }
    }


    private void AdvanceToNextPlayer()
    {
        _currentPlayerIndex = (_currentPlayerIndex + 1) % players.Count;
        _currentPlayer = players[_currentPlayerIndex];
    }

    public void Dispose()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _gameManager.OnGameOver -= OnGameOver;
    }

   
}
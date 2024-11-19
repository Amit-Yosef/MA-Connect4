using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Controllers;
using Controllers.Players;
using Data;
using Managers;
using MoonActive.Connect4;
using UnityEngine;
using Zenject;

public class TurnManager : MonoBehaviour
{
    [Inject] private PlayersManager _playersManager;
    
    [Inject] private GameManager _gameManager;

    private List<Player> players;
    private int _currentPlayerIndex = 0;
    private Player _currentPlayer;
    private CancellationTokenSource _cts;

    
    private void OnEnable()
    {
        _gameManager.OnGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        _cts.Cancel();
    }

    void Start()
    {
        _cts = new CancellationTokenSource();
        players = _playersManager.Players;
        _currentPlayer = players.First();
        PerformTurn();
    }
    
    private async void PerformTurn()
    {
        try
        {
            if (_cts.Token.IsCancellationRequested)
            {
                UnityEngine.Debug.Log("Game turn loop was canceled.");
                return;
            }

            await _currentPlayer.DoTurn(_cts);

            if (!_cts.Token.IsCancellationRequested)
            {
                UpdateNextPlayer();
                PerformTurn(); 
            }
            else
            {
                UnityEngine.Debug.Log("Game turn loop was canceled after player's turn.");
            }
        }
        catch (OperationCanceledException)
        {
            UnityEngine.Debug.Log("Game turn loop was canceled due to an exception.");
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"An error occurred during {ex.Source}: {ex.Message} \n \n {ex.StackTrace} ");
        }
    }


    private void UpdateNextPlayer()
    {
        _currentPlayerIndex = (_currentPlayerIndex + 1) % players.Count;
        _currentPlayer = players[_currentPlayerIndex];
    }
}
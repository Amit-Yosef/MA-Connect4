using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Controllers;
using Controllers.Players;
using Managers;
using MoonActive.Connect4;
using UnityEngine;
using Zenject;

public class TurnManager : MonoBehaviour
{
    [Inject] private PlayersManager _playersManager;    

    private List<Player> players;
    
    private int _currentPlayerIndex = 0;
    private Player _currentPlayerTurnStrategy;

    void Start()
    {
        players = _playersManager.Players;
        _currentPlayerTurnStrategy = players.First();
        PerformTurn();
    }

    private async void PerformTurn()
    {
        await _currentPlayerTurnStrategy.DoTurn();
        UpdateNextPlayer();
        PerformTurn();
    }

    private void UpdateNextPlayer()
    {
        _currentPlayerIndex = (_currentPlayerIndex + 1) % players.Count;
        _currentPlayerTurnStrategy = players[_currentPlayerIndex];
    }
}
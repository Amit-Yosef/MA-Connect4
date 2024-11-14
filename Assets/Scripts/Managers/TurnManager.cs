using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AYellowpaper.SerializedCollections;
using Controllers;
using MoonActive.Connect4;
using UnityEngine;
using Zenject;

public class TurnManager : MonoBehaviour
{
    [Inject] private LocalPlayer.Factory _factory;
    [Inject] private BotPlayer.Factory _botFactory;
    
    [SerializeField] private List<Disk> disks;

    private List<BasePlayer> players;
    private int _currentPlayerIndex = 0;

    private BasePlayer _currentPlayer;

    void Start()
    {
        players = new List<BasePlayer>() { _factory.Create(disks[0]), _botFactory.Create(disks[1]) };
        _currentPlayer = players.First();
        PerformTurn();
    }

    private async void PerformTurn()
    {
        await _currentPlayer.DoTurn();
        UpdateNextPlayer();
        PerformTurn();
    }

    private void UpdateNextPlayer()
    {
        _currentPlayerIndex = (_currentPlayerIndex + 1) % players.Count;
        _currentPlayer = players[_currentPlayerIndex];
    }
}
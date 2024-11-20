using System;
using System.Collections.Generic;
using Project.Data.Models;
using UnityEngine;
using Zenject;

namespace Project.Data.Configs
{
    public class PlayersConfig
    {
        [Inject] private Dictionary<PlayerBehaviorData, DiskData> _defaultPlayers;

        private List<PlayerData> _selectedPlayers;

        public void SetPlayers(List<PlayerData> playerDatas)
        {
            _selectedPlayers = playerDatas;
        }

        public List<PlayerData> GetPlayers()
        {
            if (_selectedPlayers is { Count: > 1 }) return _selectedPlayers;
            
            Debug.LogWarning(
                "Players Never Selected. Probably ran the Game scene directly. setting default Players... \n" +
                "Please start the game from StartScene.");
            return CreatePlayerData();
        }

        private List<PlayerData> CreatePlayerData()
        {
            
            var players = new List<PlayerData>();
            foreach ((var behaviorData, var diskData) in _defaultPlayers)
            {
                players.Add(new PlayerData(behaviorData,diskData));
            }

            return players;
        }
    }
}
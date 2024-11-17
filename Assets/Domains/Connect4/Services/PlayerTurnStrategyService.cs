using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class PlayerTurnStrategyService : IInitializable

    {
        private List<PlayerTurnStrategyData> _playerTurnStrategies;
        [Inject]
        public void Initialize()
        {
            _playerTurnStrategies = Resources.LoadAll<PlayerTurnStrategyData>("TurnStrategies").ToList();

            Debug.Log($"Collected {_playerTurnStrategies.Count} PlayerTurnStrategyData instances from Resources.");
        }

        public List<PlayerTurnStrategyData> GetAllStrategies()
        {
            return _playerTurnStrategies;
        }
    }


    public enum PlayerTurnStrategyType
    {
        Computer,
        Human
    }
}
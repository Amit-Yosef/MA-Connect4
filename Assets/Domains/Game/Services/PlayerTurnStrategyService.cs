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
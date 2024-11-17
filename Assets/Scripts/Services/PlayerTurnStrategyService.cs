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

    public class PlayerTurnStrategyFactory
    {
        [Inject] private readonly DiContainer _container;
        private Dictionary<PlayerTurnStrategyType, IPlayerTurnStrategy> _playerTurnStrategies;

        public IPlayerTurnStrategy Create(PlayerTurnStrategyData playerStrategyData)
        {
            return playerStrategyData.Type switch
            {
                PlayerTurnStrategyType.Computer => _container.Instantiate<BotPlayerTurnStrategy>(),
                PlayerTurnStrategyType.Human => _container.Instantiate<LocalPlayerTurnStrategy>(),
                _ => throw new ArgumentException($"Unsupported PlayerTurnStrategyType: {playerStrategyData.Type}")
            };
        }
    }

    public enum PlayerTurnStrategyType
    {
        Computer,
        Human
    }
}
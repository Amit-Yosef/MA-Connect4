using System;
using System.Collections.Generic;
using Data;
using Zenject;

namespace Controllers
{
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
}
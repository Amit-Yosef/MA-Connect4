using System;
using System.Collections.Generic;
using Game.Strategies.TurnStrategies;
using Project.Data.Models;
using StartScreen.DataProviders;
using Zenject;
using ArgumentException = System.ArgumentException;

namespace Game.Factories
{
    public class PlayerTurnFactory
    {
        [Inject] private readonly DiContainer _container;

        private Dictionary<PlayerTurnStrategyType, IPlayerTurnStrategy> _playerTurnStrategies;

        public IPlayerTurnStrategy Create(PlayerTurnStrategyData playerStrategyData)
        {
            return playerStrategyData.Type switch
            {
                PlayerTurnStrategyType.Computer => _container.Instantiate<BotPlayerTurn>(),
                PlayerTurnStrategyType.Human => _container.Instantiate<LocalPlayerTurn>(),
                _ => throw new ArgumentException($"Unsupported PlayerTurnStrategyType: {playerStrategyData.Type}")
            };
        }
    }
}
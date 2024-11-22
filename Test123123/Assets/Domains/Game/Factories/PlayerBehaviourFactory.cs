using System;
using System.Collections.Generic;
using Game.Strategies.TurnStrategies;
using Project.Data.Models;
using StartScreen.DataProviders;
using Zenject;
using ArgumentException = System.ArgumentException;

namespace Game.Factories
{
    public class PlayerBehaviourFactory
    {
        [Inject] private readonly DiContainer _container;

        private Dictionary<PlayerBehaviourType, IPlayerTurnStrategy> _playerTurnStrategies;

        public IPlayerTurnStrategy Create(PlayerBehaviorData playerStrategyData)
        {
            return playerStrategyData.Type switch
            {
                PlayerBehaviourType.Computer => _container.Instantiate<BotPlayerBehaviour>(),
                PlayerBehaviourType.Human => _container.Instantiate<LocalPlayerBehaviour>(),
                _ => throw new ArgumentException($"Unsupported PlayerTurnStrategyType: {playerStrategyData.Type}")
            };
        }
    }
}
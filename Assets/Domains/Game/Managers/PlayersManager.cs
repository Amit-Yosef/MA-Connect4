using System.Collections.Generic;
using Game.Controllers.Players;
using Game.Factories;
using Project.Data.Configs;
using Zenject;

namespace Game.Managers
{
    public class PlayersManager : IInitializable
    {
        [Inject] private PlayerController.Factory _factory;
        [Inject] private PlayersConfig _playersConfig;
        [Inject] private PlayerBehaviourFactory _behaviourFactory;

        public List<PlayerController> Players { get; private set; }

        public void Initialize()
        {
            Players = new List<PlayerController>();
            foreach (var playerData in _playersConfig.Players)
            {
                var turnStrategy = _behaviourFactory.Create(playerData.BehaviorData);
                Players.Add(_factory.Create(playerData.DiskData, turnStrategy));
            }
        }
    }
}
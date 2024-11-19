using System.Collections.Generic;
using Controllers;
using Controllers.Players;
using Data;
using Zenject;

namespace Managers
{
    public class PlayersManager : IInitializable
    {
        [Inject] private Player.Factory _factory;
        [Inject] private PlayersConfig _playersConfig;
        [Inject] private PlayerTurnStrategyFactory _turnStrategyFactory;

        public List<Player> Players { get; private set; }

        public void Initialize()
        {
            Players = new List<Player>();
            foreach (var playerData in _playersConfig.Players)
            {
                var turnStrategy = _turnStrategyFactory.Create(playerData.TurnStrategyData);
                
                Players.Add(_factory.Create(playerData.DiskData, turnStrategy));
            }
        }
    }
}
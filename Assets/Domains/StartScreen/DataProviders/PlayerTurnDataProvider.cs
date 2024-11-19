using System.Collections.Generic;
using System.Linq;
using Project.Data.Models;
using UnityEngine;
using Zenject;

namespace StartScreen.DataProviders
{
    public class PlayerTurnDataProvider : IInitializable

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
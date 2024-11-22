using System.Collections.Generic;
using System.Linq;
using Project.Data.Models;
using UnityEngine;
using Zenject;

namespace StartScreen.DataProviders
{
    public class PlayerBehavioursDataProvider : IInitializable
    {
        private List<PlayerBehaviorData> _playerTurnStrategies;
        
        public void Initialize()
        {
            _playerTurnStrategies = Resources.LoadAll<PlayerBehaviorData>("PlayerBehaviours").ToList();
        }

        public List<PlayerBehaviorData> GetAllStrategies()
        {
            return _playerTurnStrategies;
        }
    }


    public enum PlayerBehaviourType
    {
        Computer,
        Human
    }
}
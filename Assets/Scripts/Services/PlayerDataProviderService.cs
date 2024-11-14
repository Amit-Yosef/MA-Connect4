using System;
using System.Collections.Generic;
using Controllers;
using Data;
using UnityEngine;
using Zenject;

namespace Services
{
    public class PlayerDataProviderService : IInitializable
    {
        private Dictionary<Type, PlayerData> _playerTypes;

        public void Initialize()
        {
            _playerTypes = new Dictionary<Type, PlayerData>()
            {
                { typeof(LocalPlayer), new PlayerData("local", null) },
                { typeof(BotPlayer), new PlayerData("Bot", null) }
            };
        }

        public PlayerData Get( Type type) 
        {
            return _playerTypes[type];
        }
    }
}
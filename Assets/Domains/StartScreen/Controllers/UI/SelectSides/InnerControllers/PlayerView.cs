using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Controllers.UI.StartScreen.SelectSides.Disks;
using Cysharp.Threading.Tasks.Triggers;
using Data;
using MoonActive.Connect4;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Controllers.UI.StartScreen.SelectSides
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private PlayerTurnStrategyButton _turnStrategyButton;
        [SerializeField] private DiskButton _diskButton;


        
        public void Set(List<DiskData> diskOptions, List<PlayerTurnStrategyData> turnStrategyOptions)
        {
            _diskButton.Set(diskOptions);
            _turnStrategyButton.Set(turnStrategyOptions);
        }
        

        public PlayerData GetPlayerData()
        {
            return new PlayerData(_turnStrategyButton.GetCurrentSelectedItem(),
                _diskButton.GetCurrentSelectedItem());
        }

    }
}
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Controllers.UI.StartScreen.SelectSides.Disks;
using Data;
using MoonActive.Connect4;
using UnityEngine;
using Zenject;

namespace Controllers.UI.StartScreen.SelectSides
{
    public class PlayerCreationView : MonoBehaviour
    {
        
        [SerializeField] private PlayerTurnStrategyButton turnStrategyButton;
        [SerializeField] private DiskButton diskButton;

        public PlayerData GetPlayerData()
        {
            return new PlayerData(turnStrategyButton.SelectedTurnStrategy, diskButton.SelectedDisk);
        }
        
    }
}
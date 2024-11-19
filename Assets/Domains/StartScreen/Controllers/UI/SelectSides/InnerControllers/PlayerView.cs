using System.Collections.Generic;
using Project.Data.Models;
using StartScreen.Controllers.UI.SelectSides.InnerControllers.ItemSwitcherButtons.Inheritors;
using UnityEngine;

namespace StartScreen.Controllers.UI.SelectSides.InnerControllers
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
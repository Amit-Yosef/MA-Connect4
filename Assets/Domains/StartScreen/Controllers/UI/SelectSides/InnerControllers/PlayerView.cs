using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Data.Models;
using StartScreen.Controllers.UI.SelectSides.InnerControllers.ItemSwitcherButtons.Inheritors;
using UnityEngine;
using UnityEngine.Serialization;

namespace StartScreen.Controllers.UI.SelectSides.InnerControllers
{
    public class PlayerView : MonoBehaviour
    {
        public event Action<DiskData> DiskSelectedChanged;
        
        [SerializeField] private PlayerBehaviourButton behaviourButton;
        [SerializeField] private DiskButton diskButton;


        private void OnEnable()
        {
            diskButton.ItemChanged += DiskButtonOnItemChanged;
        }

        public void Set(List<DiskData> diskOptions, List<PlayerBehaviorData> turnStrategyOptions)
        {
            diskButton.Set(diskOptions);
            behaviourButton.Set(turnStrategyOptions);
        }

        private void DiskButtonOnItemChanged(DiskData diskData)
        {
            DiskSelectedChanged?.Invoke( diskData);
        }


        public PlayerData GetPlayerData()
        {
            return new PlayerData(behaviourButton.GetCurrentSelectedItem(),
                diskButton.GetCurrentSelectedItem());
        }

        private void OnDisable()
        {
            diskButton.ItemChanged -= DiskButtonOnItemChanged;
        }
    }
}
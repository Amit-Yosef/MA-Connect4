using System.Collections.Generic;
using System.Linq;
using Data;
using MoonActive.Connect4;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Controllers.UI.StartScreen.SelectSides
{
    public class PlayerTurnStrategyButton : ItemSwitcherButtonController<PlayerTurnStrategyData>
    {
        [SerializeField] private Text _buttonText;
        
        
        public override void OnButtonPressed()
        {
            base.OnButtonPressed();
            UpdateButtonText();
        }

        protected void UpdateButtonText()
        {
            _buttonText.text = CurrentSelectedKey.Name;
        }
         
    }
    
    

}

using Project.Data.Models;
using UnityEngine;
using UnityEngine.UI;

namespace StartScreen.Controllers.UI.SelectSides.InnerControllers.ItemSwitcherButtons.Inheritors
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

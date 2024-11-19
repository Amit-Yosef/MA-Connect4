using Project.Data.Models;
using UnityEngine;
using UnityEngine.UI;

namespace StartScreen.Controllers.UI.SelectSides.InnerControllers.ItemSwitcherButtons.Inheritors
{
    public class PlayerBehaviourButton : ItemSwitcherButtonController<PlayerBehaviorData>
    {
        [SerializeField] private Text _buttonText;
        
        
        protected override void UpdateButton()
        {
            base.UpdateButton();
            _buttonText.text = CurrentSelectedKey.Name;

        }
    }
}

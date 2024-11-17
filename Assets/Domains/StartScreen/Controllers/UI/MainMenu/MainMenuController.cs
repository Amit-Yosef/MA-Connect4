using Data;
using Managers;
using UnityEngine;
using Zenject;

namespace Controllers.UI.StartScreen.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [Inject] private PopUpSystem _popUpSystem;
        
        public void OpenSelectSidesPopup()
        {
            _popUpSystem.GetSelectSidesPopup();
        }
        public void OpenSettingsPopup()
        {
            _popUpSystem.Get(PopUpType.Settings);
        }
    }
}
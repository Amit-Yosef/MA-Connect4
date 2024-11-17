using Data;
using UnityEngine;
using Zenject;

namespace Controllers.UI.StartScreen.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [Inject] private PopUpSystem _popUpSystem;
        [Inject] private AppConfiguration _appConfiguration;
        
        public void OpenSelectSidesPopup()
        {
            _popUpSystem.GetSelectSidesPopup();
        }
        public void SetPlayersConfigurationMode()
        {
            _appConfiguration.PlayersConfigurationMode = PlayersConfigurationMode.Football;
        }
    }
}
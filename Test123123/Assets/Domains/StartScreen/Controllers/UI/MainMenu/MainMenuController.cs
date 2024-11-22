using UnityEngine;
using Zenject;

namespace StartScreen.Controllers.UI.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [Inject] private PopUpSystem _popUpSystem;
        
        public void Play()
        {
            _popUpSystem.Open(PopUpType.SelectSides);
        }
    
        public void OpenSettingsPopup()
        {
            _popUpSystem.Open(PopUpType.Settings);
        }
    }
}
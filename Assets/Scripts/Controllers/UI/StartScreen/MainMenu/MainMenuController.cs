using Data;
using UnityEngine;
using Zenject;

namespace Controllers.UI.StartScreen.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [Inject] private PopUpSystem _popUpSystem;
        
        public void OpenSelectSidesPopup()
        {
            _popUpSystem.Get(PopUpType.SelectSides);
        }
        public void OpenMessagePopup()
        {
            MessageBoxData message = new MessageBoxData.Builder().WithTitle("sample TItle")
                .WithBody("hey there delilah").Build();
            _popUpSystem.GetMessageBox(message);
        }
    }
}
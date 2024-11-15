using System;
using Data;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [Inject] private IBoardChecker _boardChecker;
        [Inject] private PopUpSystem _popupSystem;
        public event Action OnGameOver;
        public event Action OnGamePause;

        private void OnEnable()
        {
            _boardChecker.OnWinOrDraw += OnWinOrDraw;
        }

        private void OnWinOrDraw(BoardCheckResult result)
        {
            MessageBoxData.Builder messageboxBuilder = new MessageBoxData.Builder();
            switch (result.Type)
            {
                case BoardCheckResultType.Win:
                    messageboxBuilder.WithTitle($"WIN {result.Winner}!");
                    break;
                case BoardCheckResultType.Draw:
                    messageboxBuilder.WithTitle($"its a draw!");
                    break;
                case BoardCheckResultType.OnGoing:
                    return;
                    break;
            }
             OnGameOver?.Invoke();
            _popupSystem.GetMessageBox(messageboxBuilder.Build());
        }
    }
}
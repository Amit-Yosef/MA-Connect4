using System;
using Cysharp.Threading.Tasks;
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
        [Inject] private SceneSwitcher _sceneSwitcher;
        public event Action OnGameOver;
        public event Action OnGameRestart;
        public event Action OnGamePause;

        private void OnEnable()
        {
            _boardChecker.OnWinOrDraw += OnWinOrDraw;
        }

        private void OnWinOrDraw(BoardCheckResult result)
        {
            MessageBoxData.Builder messageboxBuilder = new MessageBoxData.Builder();
            messageboxBuilder.WithOnClickBackArrow(() =>_sceneSwitcher.LoadSceneAsync(SceneID.StartScene).Forget());
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
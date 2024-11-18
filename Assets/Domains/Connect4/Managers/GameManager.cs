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
        [Inject] private SoundSystem _soundSystem;
        [Inject] private SceneSwitchingSystem _sceneSwitchingSystem;
        public event Action OnGameOver;
        public event Action OnGameRestart;
        public event Action OnGamePause;

        private void OnEnable()
        {
            _soundSystem.PlaySound(SoundType.OnGameStart);
            _boardChecker.OnWinOrDraw += OnWinOrDraw;
        }

        private void OnWinOrDraw(BoardCheckResult result)
        {
            MessageBoxData.Builder messageboxBuilder = new MessageBoxData.Builder();
            messageboxBuilder.WithOnClickBackArrow(() => _sceneSwitchingSystem.LoadSceneAsync(SceneID.StartScene).Forget());
            switch (result.Type)
            {
                case BoardCheckResultType.Win:
                    
                    messageboxBuilder.
                        WithTitle($"WIN!").
                        WithImage(result.Winner.Sprite)
                        .TweenImage();
                    
                    _soundSystem.PlaySound(SoundType.OnGameWin);
                    break;
                
                case BoardCheckResultType.Draw:
                    
                    messageboxBuilder.
                        WithTitle("Draw...")
                        .WithBody("The Battle is Over, But the War Goes On!");
                    
                    _soundSystem.PlaySound(SoundType.OnGameDraw);
                    break;
                
                case BoardCheckResultType.OnGoing:
                    return;
            }

            OnGameOver?.Invoke();
            _popupSystem.OpenMessageBox(messageboxBuilder.Build());
        }
    }
}
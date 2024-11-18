using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using Managers;
using UnityEngine;
using Zenject;

namespace Controllers.UI.StartScreen.SelectSides
{
    public abstract class SelectSidesPopup : PopupController
    {
        [Inject] protected PlayerTurnStrategyService turnStrategyService;
        [Inject] private SceneSwitchingSystem _sceneSwitchingSystem;
        
        [SerializeField] protected PlayersView playersView;

        protected CancellationTokenSource _cts = new CancellationTokenSource();


        public virtual void Submit()
        {
            playersView.UpdatePlayersConfig();
            Close();
            _sceneSwitchingSystem.LoadSceneAsync(SceneID.GameScene).Forget();
        }

        public abstract UniTaskVoid Construct();
        
        private void OnDestroy()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
}
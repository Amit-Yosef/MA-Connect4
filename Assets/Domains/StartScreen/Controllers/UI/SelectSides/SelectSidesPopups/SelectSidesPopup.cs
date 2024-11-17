using System.Collections.Generic;
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
        [Inject] protected PlayersView.Factory playersViewFactory;
        
        [Inject] private SceneSwitchingSystem _sceneSwitchingSystem;

        [SerializeField] protected RectTransform viewsTransform;

        protected PlayersView currentView;

        public void Submit()
        {
            currentView.UpdatePlayersConfiguration();
            Close();
            _sceneSwitchingSystem.LoadSceneAsync(SceneID.GameScene).Forget();
        }
    }
}
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
        [Inject] protected PlayersConfigurationView.Factory playersViewFactory;
        
        [Inject] private SceneSwitcher _sceneSwitcher;

        [SerializeField] protected RectTransform viewsTransform;

        protected PlayersConfigurationView currentView;

        public void Submit()
        {
            currentView.UpdatePlayersConfiguration();
            Close();
            _sceneSwitcher.LoadSceneAsync(SceneID.GameScene).Forget();
        }
    }
}
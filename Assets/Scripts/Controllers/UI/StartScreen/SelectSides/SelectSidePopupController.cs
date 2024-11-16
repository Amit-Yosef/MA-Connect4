using System.Collections.Generic;
using Controllers.Players;
using Controllers.UI.StartScreen.SelectSides.Disks;
using Cysharp.Threading.Tasks;
using Data;
using Managers;
using MoonActive.Connect4;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Controllers.UI.StartScreen.SelectSides
{
    public class SelectSidePopupController : PopupController
    {
        [Inject] private PlayersConfiguration _playersConfiguration;
        [Inject] private SceneSwitcher _sceneSwitcher;

        [SerializeField] private PlayerTurnStrategyButtonsManager _turnStrategyButtonsManager;
        [SerializeField] private DiskButtonsManager _diskButtonsManager;

        public void Submit()
        {
            UpdatePlayersConfiguration();
            Close();
            _sceneSwitcher.LoadSceneAsync(SceneID.GameScene).Forget();
        }

        private void UpdatePlayersConfiguration()
        {
            var disks = _diskButtonsManager.GetSelectedDisks();
            var turnStrategies = _turnStrategyButtonsManager.GetSelectedTurnStrategies();
            List<PlayerData> players = new List<PlayerData>();

            for (int i = 0; i < disks.Count; i++)
            {
                players.Add(new PlayerData(turnStrategies[i], disks[i]));
            }

            _playersConfiguration.Players = players;
        }
    }
}
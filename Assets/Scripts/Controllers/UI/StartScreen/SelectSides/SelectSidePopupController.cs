using System.Collections.Generic;
using Controllers.Players;
using Controllers.UI.StartScreen.SelectSides.Disks;
using Cysharp.Threading.Tasks;
using Data;
using Managers;
using MoonActive.Connect4;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Zenject;

namespace Controllers.UI.StartScreen.SelectSides
{
    public class SelectSidePopupController : PopupController
    {
        [Inject] private PlayersConfiguration _playersConfiguration;
        [Inject] private SceneSwitcher _sceneSwitcher;

        [SerializeField] private List<PlayerCreationView> playerCreationViews;

        public void Submit()
        {
            UpdatePlayersConfiguration();
            Close();
            _sceneSwitcher.LoadSceneAsync(SceneID.GameScene).Forget();
        }

        private void UpdatePlayersConfiguration()
        {
            List<PlayerData> players = new List<PlayerData>();
            foreach (var playerCreationView in playerCreationViews)
            {
                players.Add(playerCreationView.GetPlayerData());
            }
            _playersConfiguration.Players = players;
        }
    }
}
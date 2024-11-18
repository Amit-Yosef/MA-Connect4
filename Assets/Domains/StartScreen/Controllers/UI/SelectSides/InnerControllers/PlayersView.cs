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
    public class PlayersView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup viewCanvasGroup;
        [Inject] private PlayersConfig _playersConfig;
        public CanvasGroup canvasGroup => viewCanvasGroup;

        [SerializeField] private List<PlayerView> playerCreationViews;

        public void Set(List<DiskData> diskOptions, List<PlayerTurnStrategyData> turnStrategyOptions)
        {
            foreach (var playerView in playerCreationViews)
            {
             playerView.Set(diskOptions,turnStrategyOptions);   
            }
        }

        public void UpdatePlayersConfig()
        {
            List<PlayerData> players = new List<PlayerData>();
            foreach (var playerCreationView in playerCreationViews)
            {
                players.Add(playerCreationView.GetPlayerData());
            }

            _playersConfig.Players = players;
        }

        public class Factory : PlaceholderFactory<PlayersViewRequest, PlayersView>
        {
        }
    }
}
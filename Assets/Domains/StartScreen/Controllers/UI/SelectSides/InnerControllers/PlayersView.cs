using System.Collections.Generic;
using Project.Data.Configs;
using Project.Data.Models;
using UnityEngine;
using Zenject;

namespace StartScreen.Controllers.UI.SelectSides.InnerControllers
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
        
    }
}
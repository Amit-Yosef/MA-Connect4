using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
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

        [SerializeField] private List<PlayerView> playerViews;

        public Action InvalidDisksSelection;
        public Action ValidDiskSelection;
        

        public void Set(List<DiskData> diskOptions, List<PlayerBehaviorData> turnStrategyOptions)
        {
            foreach (var playerView in playerViews)
            {
                playerView.DiskSelectedChanged += PlayerViewOnDiskSelectedChanged;

                playerView.Set(diskOptions, turnStrategyOptions);
            }
        }

        private async void PlayerViewOnDiskSelectedChanged(DiskData diskData)
        {
            if (playerViews.Count(pv => pv.GetPlayerData().DiskData == diskData) <= 1) return;
            
            InvalidDisksSelection?.Invoke();
            await UniTask.WaitUntil(AreAllSelectionsValid);
            ValidDiskSelection?.Invoke();
        }

        private bool AreAllSelectionsValid()
        {
            var selectedDisks = playerViews.Select(pv => pv.GetPlayerData().DiskData).ToList();
            return selectedDisks.Distinct().Count() == selectedDisks.Count;
        }

        public void UpdatePlayersConfig()
        {
            List<PlayerData> players = new List<PlayerData>();
            foreach (var playerView in playerViews)
            {
                players.Add(playerView.GetPlayerData());
            }

            _playersConfig.SetPlayers(players);
        }

        private void OnDisable()
        {
            foreach (var playerView in playerViews)
                playerView.DiskSelectedChanged += PlayerViewOnDiskSelectedChanged;
        }
    }
}
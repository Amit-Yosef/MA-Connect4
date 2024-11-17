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
    public class PlayersConfigurationView : MonoBehaviour
    {
        [Inject] private PlayersConfiguration _playersConfiguration;
        [Inject] private PlayerCreationView.Factory playerCreationViewFactory;

        [SerializeField] private RectTransform rectTransform;

        private List<PlayerCreationView> playerCreationViews = new List<PlayerCreationView>();

        [Inject]
        private void Construct(PlayersConfigurationViewConfig config)
        {
            rectTransform.SetParent(config.ParentTransform, false);
            

            var diskOptions = config.DiskOptions;
            var strategyOptions = config.StrategyOptions;
            for (int i = 0; i < config.PlayersCount; i++)
            {
                var playerViewConfig = new PlayerCreationViewConfig.Builder()
                    .SetDiskOptions(diskOptions.Count == 2 ? new List<DiskData>() { diskOptions[i] } : diskOptions)
                    .SetTurnStrategyService(strategyOptions)
                    .SetIsTurnStrategyBigButton(config.IsStrategyInBigBox)
                    .SetRectTransformParent(rectTransform)
                    .Build();
                playerCreationViews.Add(playerCreationViewFactory.Create(playerViewConfig));
            }
        }

        public void UpdatePlayersConfiguration()
        {
            List<PlayerData> players = new List<PlayerData>();
            foreach (var playerCreationView in playerCreationViews)
            {
                players.Add(playerCreationView.GetPlayerData());
            }

            _playersConfiguration.Players = players;
        }

        public class Factory : PlaceholderFactory<PlayersConfigurationViewConfig, PlayersConfigurationView>
        {
        }
    }
}
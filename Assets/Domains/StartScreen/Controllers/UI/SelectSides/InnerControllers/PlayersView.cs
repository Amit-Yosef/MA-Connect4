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
        [Inject] private PlayersConfig _playersConfig;
        [Inject] private PlayerView.Factory playerCreationViewFactory;

        [SerializeField] private RectTransform rectTransform;

        private List<PlayerView> playerCreationViews = new List<PlayerView>();

        [Inject]
        private void Construct(PlayersViewRequest request)
        {
            rectTransform.SetParent(request.ParentTransform, false);
            

            var diskOptions = request.DiskOptions;
            var strategyOptions = request.StrategyOptions;
            for (int i = 0; i < request.PlayersCount; i++)
            {
                var playerViewConfig = new PlayerViewRequest.Builder()
                    .SetDiskOptions(diskOptions.Count == 2 ? new List<DiskData>() { diskOptions[i] } : diskOptions)
                    .SetTurnStrategyService(strategyOptions)
                    .SetIsTurnStrategyBigButton(request.IsStrategyInBigBox)
                    .SetRectTransformParent(rectTransform)
                    .Build();
                playerCreationViews.Add(playerCreationViewFactory.Create(playerViewConfig));
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
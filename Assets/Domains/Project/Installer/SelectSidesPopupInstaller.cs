using AYellowpaper.SerializedCollections;
using Controllers;
using Controllers.UI.StartScreen.SelectSides;
using Controllers.UI.StartScreen.SelectSides.Disks;
using Data;
using MoonActive.Connect4;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installers
{
    public class SelectSidesPopupInstaller : MonoInstaller
    {
        [SerializeField] private DiskButton diskButton;
        [SerializeField] private PlayerTurnStrategyButton playerTurnStrategyButton;
        [SerializeField] private PlayerView playerView;
        [SerializeField] private PlayersView playersView;

        [SerializedDictionary("player config mode", "popup")] [SerializeField]
        private SerializedDictionary<DiskDataSource, SelectSidesPopup> playersConfigurationModes;

        public override void InstallBindings()
        {
            Container.Bind<SelectSidesPopupFactory>().ToSelf().AsSingle().WithArguments(playersConfigurationModes);

            Container.BindFactory<ItemSwitcherButtonRequest<DiskData>, DiskButton, DiskButton.Factory>()
                .FromComponentInNewPrefab(diskButton);
            Container
                .BindFactory<ItemSwitcherButtonRequest<PlayerTurnStrategyData>, PlayerTurnStrategyButton,
                    PlayerTurnStrategyButton.Factory>()
                .FromComponentInNewPrefab(playerTurnStrategyButton);
            Container.BindFactory<PlayerViewRequest, PlayerView, PlayerView.Factory>()
                .FromComponentInNewPrefab(playerView);
            Container.BindFactory<PlayersViewRequest, PlayersView, PlayersView.Factory>()
                .FromComponentInNewPrefab(playersView);
        }
    }
}
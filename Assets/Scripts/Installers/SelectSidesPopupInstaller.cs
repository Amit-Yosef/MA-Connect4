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
        [SerializeField] private PlayerCreationView playerCreationView;
        [SerializeField] private PlayersConfigurationView playersConfigurationView;
        
        [SerializedDictionary("player config mode", "popup")] [SerializeField]
        private SerializedDictionary<PlayersConfigurationMode, SelectSidesPopup> playersConfigurationModes;

        public override void InstallBindings()
        {
            Container.Bind<SelectSidesPopupFactory>().ToSelf().AsSingle().WithArguments(playersConfigurationModes);
            
            Container.BindFactory<ItemSwitcherButtonConfig<DiskData>, DiskButton, DiskButton.Factory>()
                .FromComponentInNewPrefab(diskButton);
            Container.BindFactory<ItemSwitcherButtonConfig<PlayerTurnStrategyData>, PlayerTurnStrategyButton, PlayerTurnStrategyButton.Factory>()
                .FromComponentInNewPrefab(playerTurnStrategyButton);
            Container.BindFactory<PlayerCreationViewConfig, PlayerCreationView, PlayerCreationView.Factory>()
                .FromComponentInNewPrefab(playerCreationView);
            Container.BindFactory<PlayersConfigurationViewConfig, PlayersConfigurationView, PlayersConfigurationView.Factory>()
                .FromComponentInNewPrefab(playersConfigurationView);
        }
        
    }
}
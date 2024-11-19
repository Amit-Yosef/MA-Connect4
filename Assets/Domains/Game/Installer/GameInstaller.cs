using Controllers;
using Controllers.Players;
using Data;
using Managers;
using MoonActive.Connect4;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private RectTransform uiCanvas;
    public override void InstallBindings()
    {
        
        Container.Bind<PopUpSystem>().ToSelf().AsSingle().WithArguments(uiCanvas);
        Container.Bind<PopupFactory>().AsSingle();

        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ConnectGameGrid>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IBoardChecker>().To<BoardChecker>().AsSingle();
        Container.Bind<PlayerTurnStrategyFactory>().ToSelf().AsSingle();
        
        Container.BindInterfacesAndSelfTo<PlayersManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<BoardSystem>().AsSingle();
        
        Container.BindFactory<DiskData, IPlayerTurnStrategy, Player, Player.Factory>().AsSingle();


    }
}
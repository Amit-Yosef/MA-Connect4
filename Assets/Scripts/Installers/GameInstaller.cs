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
        Container.Bind<RectTransform>().WithId(typeof(Canvas))
            .FromComponentInHierarchy(uiCanvas).AsSingle();
        
        Container.Bind<PopUpSystem>().ToSelf().AsSingle();
        Container.Bind<PopupFactory>().AsSingle();

        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ConnectGameGrid>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IBoardChecker>().To<BoardChecker>().AsSingle();
        Container.Bind<PlayerTurnStrategyFactory>().ToSelf().AsSingle();
        
        Container.BindInterfacesAndSelfTo<PlayersManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<BoardSystem>().AsSingle();
        
        Container.BindFactory<Disk, IPlayerTurnStrategy, Player, Player.Factory>().AsSingle();


    }
}
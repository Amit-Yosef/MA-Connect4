using Controllers;
using Managers;
using MoonActive.Connect4;
using Services;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ConnectGameGrid>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IBoardChecker>().To<BoardChecker>().AsSingle();

        Container.BindInterfacesAndSelfTo<BoardSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerDataProviderService>().AsSingle();
        
        Container.BindFactory<Disk, LocalPlayer , LocalPlayer.Factory>();
        Container.BindFactory<Disk, BotPlayer , BotPlayer.Factory>();
    }
}
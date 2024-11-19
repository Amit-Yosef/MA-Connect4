using System;
using Controllers;
using Controllers.Players;
using Data;
using Managers;
using MoonActive.Connect4;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private ConnectGameGrid gridView;

    public override void InstallBindings()
    {
        Container.Bind<ConnectGameGrid>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IBoardChecker>().To<BoardChecker>().AsSingle();
        Container.Bind<PlayerTurnFactory>().ToSelf().AsSingle();

        Container.BindInterfacesAndSelfTo<PlayersManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<TurnManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<BoardSystem>()
            .AsSingle()
            .WithArguments((Func<Disk, int, int, IDisk>)((disk, column, row) => gridView.Spawn(disk, column, row)));

        Container.BindFactory<DiskData, IPlayerTurnStrategy, Player, Player.Factory>().AsSingle();
    }
}
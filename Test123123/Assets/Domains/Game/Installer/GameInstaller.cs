using System;
using Game.Controllers.Players;
using Game.Factories;
using Game.Managers;
using Game.Strategies.BoardCheck;
using Game.Strategies.TurnStrategies;
using Game.Systems;
using MoonActive.Connect4;
using Project.Data.Models;
using UnityEngine;
using Zenject;

namespace Game.Installer
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private ConnectGameGrid gridView;

        public override void InstallBindings()
        {
            Container.Bind<ConnectGameGrid>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IBoardChecker>().To<BoardChecker>().AsSingle();
            Container.Bind<PlayerBehaviourFactory>().ToSelf().AsSingle();

            Container.BindInterfacesAndSelfTo<BoardSystem>()
                .AsSingle()
                .WithArguments((Func<Disk, int, int, IDisk>)((disk, column, row) => gridView.Spawn(disk, column, row)));
            Container.BindInterfacesAndSelfTo<PlayersManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<TurnManager>().AsSingle();


            Container.BindFactory<DiskData, IPlayerTurnStrategy, PlayerController, PlayerController.Factory>().AsSingle();
        }
    }
}
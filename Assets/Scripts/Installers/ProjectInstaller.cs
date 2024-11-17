using System;
using Controllers;
using Data;
using Data.FootballApi;
using Managers;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private SceneSwitcher _sceneSwitcherPrefab;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerTurnStrategyService>().AsSingle();
        
        Container.Bind<PlayersConfiguration>().ToSelf().AsSingle();
        Container.Bind<AppConfiguration>().ToSelf().AsSingle().NonLazy();;

        Container.Bind<FootballApiFetcher>().ToSelf().AsSingle();
        Container.BindInterfacesAndSelfTo<FootballApiService>().AsSingle().NonLazy();;

        BindSceneSwitcher();

    }

    private void BindSceneSwitcher()
    {
        SceneSwitcher sceneSwitcherInstance = Container
            .InstantiatePrefabForComponent<SceneSwitcher>(_sceneSwitcherPrefab);
        //if unity editor so dont plz
        DontDestroyOnLoad(sceneSwitcherInstance.gameObject);

        Container
            .Bind<SceneSwitcher>()
            .FromInstance(sceneSwitcherInstance)
            .AsSingle()
            .NonLazy();
    }
}
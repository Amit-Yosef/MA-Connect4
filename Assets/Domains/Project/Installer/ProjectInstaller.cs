using System;
using Controllers;
using Data;
using Data.FootballApi;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Object = UnityEngine.Object;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private SceneSwitchingSystem sceneSwitchingSystemPrefab;
    [SerializeField] private DynamicDisk dynamicDisk;

    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerTurnStrategyService>().AsSingle();
        
        Container.Bind<PlayersConfig>().ToSelf().AsSingle();
        Container.Bind<DiskDataSourceConfig>().ToSelf().AsSingle().NonLazy();;

        Container.Bind<FootballApiClient>().ToSelf().AsSingle();
        Container.BindInterfacesAndSelfTo<FootballApiService>().AsSingle().NonLazy();
        Container.Bind<FootballDiskProvider>().ToSelf().AsSingle();
        Container.BindFactory<Sprite, DynamicDisk, DynamicDisk.Factory>().ToSelf()
            .FromComponentInNewPrefab(dynamicDisk).AsSingle();

        BindSceneSwitcher();

    }

    private void BindSceneSwitcher()
    {
        SceneSwitchingSystem sceneSwitchingSystemInstance = Container
            .InstantiatePrefabForComponent<SceneSwitchingSystem>(sceneSwitchingSystemPrefab);
        //if unity editor so dont plz
        DontDestroyOnLoad(sceneSwitchingSystemInstance.gameObject);

        Container
            .Bind<SceneSwitchingSystem>()
            .FromInstance(sceneSwitchingSystemInstance)
            .AsSingle()
            .NonLazy();
    }
}
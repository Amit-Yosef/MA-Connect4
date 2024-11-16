using System;
using Controllers;
using Data;
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
        
        SceneSwitcher sceneSwitcherInstance = Container
            .InstantiatePrefabForComponent<SceneSwitcher>(_sceneSwitcherPrefab);

        DontDestroyOnLoad(sceneSwitcherInstance.gameObject);

        Container
            .Bind<SceneSwitcher>()
            .FromInstance(sceneSwitcherInstance)
            .AsSingle()
            .NonLazy();
    }
}
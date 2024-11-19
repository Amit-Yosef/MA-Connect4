using Project.Data.Configs;
using Project.Systems;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private SceneSwitchingSystem sceneSwitchingSystemPrefab;

    
    public override void InstallBindings()
    {
        Container.Bind<PlayersConfig>().ToSelf().AsSingle();

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
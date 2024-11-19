using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Project.Data.Configs;
using Project.Systems;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializedDictionary("Scene Id", "Scene Name")]
    [SerializeField] private SerializedDictionary<SceneID, string> scenes;

    [SerializeField] private SceneSwitchingController switchingScenesController;

    public override void InstallBindings()
    {
        Container.Bind<PlayersConfig>()
            .ToSelf()
            .AsSingle();

        Container.BindFactory<SceneSwitchingController, SceneSwitchingController.Factory>()
            .FromComponentInNewPrefab(switchingScenesController);
        
        Container.BindInterfacesAndSelfTo<SceneSwitchingSystem>()
            .AsSingle()
            .WithArguments(scenes);
    }
}
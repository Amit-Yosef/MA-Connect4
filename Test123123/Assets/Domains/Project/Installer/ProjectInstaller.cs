using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Project.Data.Configs;
using Project.Data.Models;
using Project.Systems;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializedDictionary("Scene Id", "Scene Name")] [SerializeField]
    private SerializedDictionary<SceneID, string> scenes;

    [SerializeField] private SceneSwitchingController switchingScenesController;

    [SerializedDictionary("behaviour", "disk")] [SerializeField]
    private SerializedDictionary<PlayerBehaviorData, DiskData> defaultPlayers;

    public override void InstallBindings()
    {
        Container.Bind<PlayersConfig>()
            .ToSelf()
            .AsSingle()
            .WithArguments(defaultPlayers);

        Container.BindFactory<SceneSwitchingController, SceneSwitchingController.Factory>()
            .FromComponentInNewPrefab(switchingScenesController);

        Container.BindInterfacesAndSelfTo<SceneSwitchingSystem>().AsSingle().WithArguments(scenes);
    }
}
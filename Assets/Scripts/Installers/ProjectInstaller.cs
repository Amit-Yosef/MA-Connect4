using System;
using Controllers;
using Data;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerTurnStrategyService>().AsSingle();
        Container.Bind<PlayersConfiguration>().ToSelf().AsSingle();
    }
}
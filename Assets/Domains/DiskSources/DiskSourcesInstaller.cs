using System;
using System.Collections.Generic;
using Controllers;
using Data;
using Domains.DiskSources.Controllers;
using Domains.DiskSources.Providers;
using Domains.DiskSources.Sources.FakePeople;
using Domains.DiskSources.Sources.Football;
using UnityEngine;
using Zenject;

namespace Domains.DiskSources
{
    public class DiskSourcesInstaller : MonoInstaller
    {
        [SerializeField] private DynamicDisk dynamicDisk;
        [SerializeField] private List<DiskData> _diskDatas;


        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<DefaultGameDiskProvider>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<FixtureGameDiskProvider>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<FootballFixtureGameDiskSource>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<FoodishGameDiskSource>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MoonGameDiskSource>().AsSingle().WithArguments(_diskDatas).NonLazy();

            
            Container.BindFactory<Sprite, DynamicDisk, DynamicDisk.Factory>().ToSelf()
                .FromComponentInNewPrefab(dynamicDisk).AsSingle();
            
            Container.BindInterfacesAndSelfTo<DiskProvidersConfiguration>().AsSingle().NonLazy();;

        }
    }
}
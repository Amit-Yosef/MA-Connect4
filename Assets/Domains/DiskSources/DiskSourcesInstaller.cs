using System;
using System.Collections.Generic;
using Controllers;
using Data;
using Domains.DiskSources.Controllers;
using Domains.DiskSources.Providers;
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
            Container.BindInterfacesAndSelfTo<DefaultDiskProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<FixtureDiskProvider>().AsSingle();

            Container.BindInterfacesAndSelfTo<FootballDiskSource>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MoonDiskSource>().AsSingle().WithArguments(_diskDatas).NonLazy();

            
            Container.BindFactory<Sprite, DynamicDisk, DynamicDisk.Factory>().ToSelf()
                .FromComponentInNewPrefab(dynamicDisk).AsSingle();
            
            Container.BindInterfacesAndSelfTo<DiskProvidersConfiguration>().AsSingle().NonLazy();;

        }
    }
}
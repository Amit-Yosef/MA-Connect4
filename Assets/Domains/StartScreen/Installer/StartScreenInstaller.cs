using System.Collections.Generic;
using Project.Data.Models;
using StartScreen.DataProviders;
using UnityEngine;
using Zenject;

namespace StartScreen.Installer
{
    public class StartScreenInstaller : MonoInstaller
    {
        
        [SerializeField] private List<DiskData> diskDatas;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerBehavioursProvider>().AsSingle();
            Container.Bind<DiskDataProvider>().ToSelf().AsSingle().WithArguments(diskDatas);
        }
    }
}
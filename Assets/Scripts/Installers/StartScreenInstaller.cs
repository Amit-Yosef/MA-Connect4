using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Controllers;
using Controllers.Players;
using Controllers.UI.StartScreen.SelectSides;
using Controllers.UI.StartScreen.SelectSides.Disks;
using Data;
using Managers;
using MoonActive.Connect4;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class StartScreenInstaller : MonoInstaller
    {
        [SerializeField] private RectTransform uiCanvas;
        [SerializeField] private DynamicDisk dynamicDisk;
        

        [SerializeField] private List<DiskData> _diskDatas;
        
        public override void InstallBindings()
        {
            Container.Bind<RectTransform>().WithId(typeof(Canvas))
                .FromComponentInHierarchy(uiCanvas).AsSingle();
            Container.Bind<PopUpSystem>().ToSelf().AsSingle();
            Container.Bind<PopupFactory>().AsSingle();
            Container.Bind<NormalDiskProvider>().ToSelf().AsSingle().WithArguments(_diskDatas);
            Container.Bind<TeamDiskProvider>().ToSelf().AsSingle();

            Container.BindFactory<Sprite, DynamicDisk, DynamicDisk.Factory>().ToSelf()
                .FromComponentInNewPrefab(dynamicDisk).AsSingle();

        }
    }
}
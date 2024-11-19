using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Controllers;
using Controllers.Players;
using Controllers.UI.StartScreen.SelectSides;
using Controllers.UI.StartScreen.SelectSides.Disks;
using Data;
using Domains.StartScreen.Services;
using Managers;
using MoonActive.Connect4;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class StartScreenInstaller : MonoInstaller
    {
        [SerializeField] private RectTransform uiCanvas;
        [SerializeField] private List<DiskData> diskDatas;
        

        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerTurnStrategyService>().AsSingle();
            Container.Bind<DiskProvider>().ToSelf().AsSingle().WithArguments(diskDatas);

            Container.Bind<PopUpSystem>().ToSelf().AsSingle().WithArguments(uiCanvas);
            Container.Bind<PopupFactory>().AsSingle();

           

        }
    }
}
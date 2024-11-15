using Controllers;
using Controllers.Players;
using Managers;
using MoonActive.Connect4;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class StartScreenInstaller : MonoInstaller
    {
        [SerializeField] private RectTransform uiCanvas;
        public override void InstallBindings()
        {
            Container.Bind<RectTransform>().WithId(typeof(Canvas))
                .FromComponentInHierarchy(uiCanvas).AsSingle();
            Container.Bind<PopUpSystem>().ToSelf().AsSingle();
            Container.Bind<PopupFactory>().AsSingle();

        }
    }
}
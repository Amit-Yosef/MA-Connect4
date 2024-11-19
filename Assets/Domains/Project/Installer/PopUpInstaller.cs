using AYellowpaper.SerializedCollections;
using Project.Data.Models;
using Project.Factories;
using UnityEngine;
using Zenject;

namespace Project.Installer
{
    public class PopUpInstaller : MonoInstaller
    {
        [SerializeField] private MessageBoxController messageBoxPrefab;
        [SerializeField] private RectTransform uiCanvas;

        [SerializedDictionary("Popup Type", "Prefab")] [SerializeField]
        private SerializedDictionary<PopUpType, PopupController> popUps;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PopUpSystem>().AsSingle().WithArguments(uiCanvas);
            Container.Bind<PopupFactory>().ToSelf().AsSingle().WithArguments(popUps);
            Container.BindFactory<MessageBoxRequest, RectTransform, MessageBoxController, MessageBoxController.Factory>()
                .FromComponentInNewPrefab(messageBoxPrefab)
                .AsSingle();
        }
    }
}
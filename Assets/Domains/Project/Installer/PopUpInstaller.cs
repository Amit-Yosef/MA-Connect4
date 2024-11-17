using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Controllers;
using Data;
using MoonActive.Connect4;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PopUpInstaller : MonoInstaller
    {
        [SerializeField] private MessageBoxController messageBoxPrefab;

        [SerializedDictionary("Popup Type", "Prefab")] [SerializeField]
        private SerializedDictionary<PopUpType, PopupController> popUps;

        public override void InstallBindings()
        {
            Container.BindFactory<MessageBoxData,RectTransform, MessageBoxController, MessageBoxController.Factory>()
                .FromComponentInNewPrefab(messageBoxPrefab)
                .AsTransient();
            Container.Bind<Dictionary<PopUpType, PopupController>>().FromInstance(popUps).AsTransient();
        }
    }
}
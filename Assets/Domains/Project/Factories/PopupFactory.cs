using System;
using System.Collections.Generic;
using Controllers.UI.StartScreen.SelectSides;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class PopupFactory 
    {
        [Inject] private DiContainer _container;
        [Inject] private Dictionary<PopUpType, PopupController> _popups;
        public PopupController Create(PopUpType popUpType,RectTransform parent)
        {
            if (!_popups.TryGetValue(popUpType, out PopupController prefab))
                throw new ArgumentException($"Unsupported popup type: popUpType");
            
            if (prefab == null)
            {
                Debug.LogWarning($"Popup prefab for type {popUpType} is null.");
                return null;
            }
            
            var instance = _container.InstantiatePrefabForComponent<PopupController>(prefab, parent);
            return instance;

        }
    }
}
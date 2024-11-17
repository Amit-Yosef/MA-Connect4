using System.Collections.Generic;
using Controllers.UI.StartScreen.SelectSides;
using Data;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class SelectSidesPopupFactory
    {
        [Inject] private Dictionary<DiskProviderMode, SelectSidesPopup> _views;
        [Inject] private AppConfiguration _appConfiguration;
        [Inject] private DiContainer _container;

        public SelectSidesPopup Create(RectTransform parent)
        {
            if (_views.TryGetValue(_appConfiguration.diskProviderMode, out var view))
                return _container.InstantiatePrefabForComponent<SelectSidesPopup>(view, parent);
            
            Debug.LogError($"{_appConfiguration.diskProviderMode} doesnt have a matching view");
            return null;

        }
    }
}
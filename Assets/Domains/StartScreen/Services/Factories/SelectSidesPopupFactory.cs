using System.Collections.Generic;
using Controllers.UI.StartScreen.SelectSides;
using Data;
using MoonActive.Connect4;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class SelectSidesPopupFactory// ToDO remove this for an adapter DP
    {
        [Inject] private Dictionary<DiskDataSource, SelectSidesPopup> _views;
        [Inject] private DiskDataSourceConfig _diskDataSourceConfig;
        [Inject] private DiContainer _container;

        public SelectSidesPopup Create(RectTransform parent)
        {
            if (_views.TryGetValue(_diskDataSourceConfig.diskDataSource, out var view))
                return _container.InstantiatePrefabForComponent<SelectSidesPopup>(view, parent);
            
            Debug.LogError($"{_diskDataSourceConfig.diskDataSource} doesnt have a matching view");
            return null;

        }
    }
}
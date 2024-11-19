using System;
using System.Collections.Generic;
using Project.Data.Models;
using UnityEngine;
using UnityEngine.UI;

namespace StartScreen.Controllers.UI.SelectSides.InnerControllers.ItemSwitcherButtons
{
    public class ItemSwitcherButtonController<T> : MonoBehaviour where T : IHasImage
    {
        public event Action<T> ItemChanged; 

        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Button button;
        [SerializeField] private Image buttonImage;
        [SerializeField] private int startIndex;
        [SerializeField] private bool canSwitch = true;

        protected T CurrentSelectedKey;

        private List<T> _options;
        private int _currentIndex;
        
        
        public void Set(List<T> options)
        {
            _options = options;
            _currentIndex = Mathf.Clamp(startIndex, 0, _options.Count - 1);
            CurrentSelectedKey = _options[_currentIndex];
            UpdateButton();
        }

        public void OnButtonPressed()
        {
            if (!canSwitch || _options == null || _options.Count == 0) return;

            _currentIndex = (_currentIndex + 1) % _options.Count;
            CurrentSelectedKey = _options[_currentIndex];
            UpdateButton();
            ItemChanged?.Invoke(CurrentSelectedKey);
        }

        protected virtual void UpdateButton()
        {
            buttonImage.sprite = CurrentSelectedKey.Sprite;
        }

        public T GetCurrentSelectedItem() => CurrentSelectedKey;
    }
}
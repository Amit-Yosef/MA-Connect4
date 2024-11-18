using Data;
using UnityEngine.Serialization;
using Zenject;

namespace Controllers.UI.StartScreen.SelectSides
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class ItemSwitcherButtonController<T> : MonoBehaviour where T : IHasImage
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Button _button;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private bool canSwitch = true;

        protected T CurrentSelectedKey;

        private List<T> _options;
        private int _currentIndex;
        
        public void Set(List<T> options)
        {
            
            _options = options;
            _currentIndex = 0;
            CurrentSelectedKey = _options[_currentIndex];
            UpdateButtonSprite();
        }

        public virtual void OnButtonPressed()
        {
            if (!canSwitch || _options == null || _options.Count == 0) return;

            _currentIndex = (_currentIndex + 1) % _options.Count;
            CurrentSelectedKey = _options[_currentIndex];
            UpdateButtonSprite();
        }

        private void UpdateButtonSprite()
        {
            _buttonImage.sprite = CurrentSelectedKey.Sprite;
        }

        public T GetCurrentSelectedItem() => CurrentSelectedKey;
    }
}
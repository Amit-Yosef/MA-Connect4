using Data;
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
        [SerializeField] private bool _canSwitch = true;

        protected T CurrentSelectedKey;

        private List<T> _options;
        private int _currentIndex;

        [Inject]
        private void Construct(ItemSwitcherButtonRequest<T> request)
        {
            
            _options = request.Options;
            _currentIndex = Mathf.Clamp(request.StartIndex, 0, _options.Count - 1);
            CurrentSelectedKey = _options[_currentIndex];
            _canSwitch = request.CanSwitch;
            UpdateButtonSprite();
            _button.onClick.AddListener(OnButtonPressed);
        }

        protected virtual void OnButtonPressed()
        {
            if (!_canSwitch || _options == null || _options.Count == 0) return;

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
using System.Collections.Generic;
using System.Linq;
using Data;
using MoonActive.Connect4;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Controllers.UI.StartScreen.SelectSides
{
    public class PlayerTurnStrategyButton : MonoBehaviour
    {
        public PlayerTurnStrategyData SelectedTurnStrategy => _selectedTurnStrategyStrategy;
        
        [Inject] private PlayerTurnStrategyService _playerTurnStrategyDataProvider;
        
        [SerializeField] private Text _text;
        [SerializeField] private Image _spriteImage;
        
        private List<PlayerTurnStrategyData> _turnStrategiesOptions = new List<PlayerTurnStrategyData>();
        private PlayerTurnStrategyData _selectedTurnStrategyStrategy;
        private int _currentIndex = 0;

        private void Start()
        {
            PopulateTurnStrategyOptions();
            SetSelectedTurnStrategy(_turnStrategiesOptions[_currentIndex]);
        }

        private void PopulateTurnStrategyOptions()
        {
            _turnStrategiesOptions = _playerTurnStrategyDataProvider.GetAllStrategies();
            if (_turnStrategiesOptions.Count == 0)
            {
                Debug.LogError("No player turn strategies available!");
            }
        }

        public void OnButtonClick()
        {
            _currentIndex = (_currentIndex + 1) % _turnStrategiesOptions.Count;
            SetSelectedTurnStrategy(_turnStrategiesOptions[_currentIndex]);
        }

        private void SetSelectedTurnStrategy(PlayerTurnStrategyData turnStrategyStrategy)
        {
            _selectedTurnStrategyStrategy = turnStrategyStrategy;
            _text.text = turnStrategyStrategy.Name;
            _spriteImage.sprite = turnStrategyStrategy.Sprite;
        }
    }
}
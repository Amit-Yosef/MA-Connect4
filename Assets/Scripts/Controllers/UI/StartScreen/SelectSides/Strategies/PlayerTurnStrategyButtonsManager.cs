using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Controllers.UI.StartScreen.SelectSides
{
    public class PlayerTurnStrategyButtonsManager : MonoBehaviour
    {
        [SerializeField] private List<PlayerTurnStrategyButton> _buttons;

        public  List<PlayerTurnStrategyData> GetSelectedTurnStrategies()
        {
            var turnStrategies = new List<PlayerTurnStrategyData>();
            foreach (var button in _buttons)
            {
                turnStrategies.Add(button.SelectedTurnStrategy);
            }

            return turnStrategies;
        }
    }
}
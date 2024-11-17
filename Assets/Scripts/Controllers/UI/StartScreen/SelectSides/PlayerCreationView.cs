using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Controllers.UI.StartScreen.SelectSides.Disks;
using Data;
using MoonActive.Connect4;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Controllers.UI.StartScreen.SelectSides
{
    public class PlayerCreationView : MonoBehaviour
    {
        [Inject] private PlayerTurnStrategyButton.Factory _strategyButtonFactory;
        [Inject] private DiskButton.Factory _diskButtonFactory;

        [SerializeField] private RectTransform bigButtonTransform;
        [SerializeField] private RectTransform smallButtonTransform;

        private PlayerTurnStrategyButton _turnStrategyButton;
        private DiskButton _diskButton;

        private bool _isTurnStrategyBigButton;

        [Inject]
        private void Construct(PlayerCreationViewConfig config)
        {
            transform.SetParent(config.rectTransformParent, false);
            
            transform.position.Set(0,0,0);
            _isTurnStrategyBigButton = config.isTurnStrategyBigButton;
            var turnStrategyOptions = config.turnStrategyOptions;
            var diskOptions = config.diskOptions;

            CreateButtons(diskOptions, turnStrategyOptions);
            InitializeButtonTransforms();

        }

        private void CreateButtons(List<DiskData> diskOptions, List<PlayerTurnStrategyData> turnStrategyOptions)
        {
            var strategyConfig = new ItemSwitcherButtonConfig<PlayerTurnStrategyData>.Builder()
                .AddOptions(turnStrategyOptions)
                .SetRectTransform(bigButtonTransform)
                .Build();

            _turnStrategyButton =_strategyButtonFactory.Create(strategyConfig);

            var diskButtonConfig = new ItemSwitcherButtonConfig<DiskData>.Builder().AddOptions(diskOptions)
                .SetCanSwitch(diskOptions.Count != 1)
                .SetRectTransform(smallButtonTransform)
                .Build();

            _diskButton =_diskButtonFactory.Create(diskButtonConfig);
        }

        private void InitializeButtonTransforms()
        {
            if (_isTurnStrategyBigButton)
            {
                UpdateRectTransform(_turnStrategyButton.transform as RectTransform, bigButtonTransform);
                UpdateRectTransform(_diskButton.transform as RectTransform, smallButtonTransform);
            }
            else
            {
                UpdateRectTransform(_turnStrategyButton.transform as RectTransform, smallButtonTransform);
                UpdateRectTransform(_diskButton.transform as RectTransform, bigButtonTransform);
            }
        }

        private void UpdateRectTransform(RectTransform target, RectTransform parent)
        {
            target.SetParent(parent, false);
            target.anchorMin = parent.anchorMin;
            target.anchorMax = parent.anchorMax;
            target.anchoredPosition = parent.anchoredPosition;
            target.sizeDelta = parent.sizeDelta;
            target.pivot = parent.pivot;
            target.localScale = Vector3.one;
            target.localPosition = Vector3.zero;
        }

        public PlayerData GetPlayerData()
        {
            return new PlayerData(_turnStrategyButton.GetCurrentSelectedItem(),
                _diskButton.GetCurrentSelectedItem());
        }

        public class Factory : PlaceholderFactory<PlayerCreationViewConfig, PlayerCreationView>
        {
        }
    }
}
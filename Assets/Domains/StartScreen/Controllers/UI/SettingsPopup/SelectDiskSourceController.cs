using System;
using Data;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Controllers.UI.StartScreen
{
    public class SelectDiskSourceController : MonoBehaviour
    {
        [Inject] private DiskProvidersConfiguration _disProvidersConfiguration;

        [SerializeField] private Dropdown defaultGameModeDropdown;
        [SerializeField] private Dropdown fixtureGameModeDropdown;

        private void OnEnable()
        {
            PopulateDefaultGameModeDropdown();
            PopulateFixtureGameModeDropdown();

            defaultGameModeDropdown.value = (int)_disProvidersConfiguration.defaultGameDiskSourceType;
            fixtureGameModeDropdown.value = (int)_disProvidersConfiguration.fixtureGameDiskSourceType;

            if (defaultGameModeDropdown != null)
                defaultGameModeDropdown.onValueChanged.AddListener(OnDefaultGameModeDropdownValueChange);

            if (fixtureGameModeDropdown != null)
                fixtureGameModeDropdown.onValueChanged.AddListener(OnFixtureGameModeDropdownValueChange);
        }

        private void OnDisable()
        {
            if (defaultGameModeDropdown != null)
                defaultGameModeDropdown.onValueChanged.RemoveListener(OnDefaultGameModeDropdownValueChange);

            if (fixtureGameModeDropdown != null)
                fixtureGameModeDropdown.onValueChanged.RemoveListener(OnFixtureGameModeDropdownValueChange);
        }

        private void PopulateDefaultGameModeDropdown()
        {
            if (defaultGameModeDropdown == null) return;

            defaultGameModeDropdown.options.Clear();
            foreach (DefaultGameDiskSourceType mode in Enum.GetValues(typeof(DefaultGameDiskSourceType)))
            {
                defaultGameModeDropdown.options.Add(new Dropdown.OptionData(mode.ToString()));
            }
            defaultGameModeDropdown.RefreshShownValue();
        }

        private void PopulateFixtureGameModeDropdown()
        {
            if (fixtureGameModeDropdown == null) return;

            fixtureGameModeDropdown.options.Clear();
            foreach (FixtureGameDiskSourceType mode in Enum.GetValues(typeof(FixtureGameDiskSourceType)))
            {
                fixtureGameModeDropdown.options.Add(new Dropdown.OptionData(mode.ToString()));
            }
            fixtureGameModeDropdown.RefreshShownValue();
        }

        private void OnDefaultGameModeDropdownValueChange(int modeIndex)
        {
            _disProvidersConfiguration.defaultGameDiskSourceType = (DefaultGameDiskSourceType)modeIndex;
        }

        private void OnFixtureGameModeDropdownValueChange(int modeIndex)
        {
            _disProvidersConfiguration.fixtureGameDiskSourceType = (FixtureGameDiskSourceType)modeIndex;
        }
    }
}

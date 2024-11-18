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

            defaultGameModeDropdown.value = (int)_disProvidersConfiguration.defaultGameModeDiskSource;
            fixtureGameModeDropdown.value = (int)_disProvidersConfiguration.fixtureGameModeDiskSource;

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
            foreach (DefaultGameModeDiskSource mode in Enum.GetValues(typeof(DefaultGameModeDiskSource)))
            {
                defaultGameModeDropdown.options.Add(new Dropdown.OptionData(mode.ToString()));
            }
            defaultGameModeDropdown.RefreshShownValue();
        }

        private void PopulateFixtureGameModeDropdown()
        {
            if (fixtureGameModeDropdown == null) return;

            fixtureGameModeDropdown.options.Clear();
            foreach (FixtureGameModeDiskSource mode in Enum.GetValues(typeof(FixtureGameModeDiskSource)))
            {
                fixtureGameModeDropdown.options.Add(new Dropdown.OptionData(mode.ToString()));
            }
            fixtureGameModeDropdown.RefreshShownValue();
        }

        private void OnDefaultGameModeDropdownValueChange(int modeIndex)
        {
            _disProvidersConfiguration.defaultGameModeDiskSource = (DefaultGameModeDiskSource)modeIndex;
        }

        private void OnFixtureGameModeDropdownValueChange(int modeIndex)
        {
            _disProvidersConfiguration.fixtureGameModeDiskSource = (FixtureGameModeDiskSource)modeIndex;
        }
    }
}

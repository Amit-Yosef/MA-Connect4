using System;
using Data;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace Controllers.UI.StartScreen
{
    public class DiskProviderModeController : MonoBehaviour
    {
        [Inject] private AppConfiguration _appConfiguration;
        [SerializeField] private Dropdown disksDropDown;

        private void OnEnable()
        {
            PopulateDiskDropDown();
            disksDropDown.value = (int) _appConfiguration.diskProviderMode;
            
            if (disksDropDown != null)
                disksDropDown.onValueChanged.AddListener(OnDiskDropDownValueChange);
        }

        private void OnDisable()
        {
            if (disksDropDown != null)
                disksDropDown.onValueChanged.RemoveListener(OnDiskDropDownValueChange);
        }

        private void PopulateDiskDropDown()
        {
            if (disksDropDown == null) return;

            disksDropDown.options.Clear();
            foreach (DiskProviderMode mode in Enum.GetValues(typeof(DiskProviderMode)))
            {
                disksDropDown.options.Add(new Dropdown.OptionData(mode.ToString()));
            }
            disksDropDown.RefreshShownValue();
        }

        private void OnDiskDropDownValueChange(int modeIndex)
        {
            _appConfiguration.diskProviderMode = (DiskProviderMode)modeIndex;
        }
    }

}
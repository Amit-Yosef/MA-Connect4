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
        [Inject] private DiskDataSourceConfig _diskDataSourceConfig;
        [SerializeField] private Dropdown disksDropDown;

        private void OnEnable()
        {
            PopulateDiskDropDown();
            disksDropDown.value = (int) _diskDataSourceConfig.diskDataSource;
            
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
            foreach (DiskDataSource mode in Enum.GetValues(typeof(DiskDataSource)))
            {
                disksDropDown.options.Add(new Dropdown.OptionData(mode.ToString()));
            }
            disksDropDown.RefreshShownValue();
        }

        private void OnDiskDropDownValueChange(int modeIndex)
        {
            _diskDataSourceConfig.diskDataSource = (DiskDataSource)modeIndex;
        }
    }

}
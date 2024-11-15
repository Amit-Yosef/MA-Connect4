using System.Collections.Generic;
using MoonActive.Connect4;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.UI.StartScreen.SelectSides.Disks
{
    public class DiskButton : MonoBehaviour
    {
        [SerializeField] private Image selectedDiskSprite;

        private List<KeyValuePair<Disk, Sprite>> _diskOptionsList;
        private Disk _selectedDisk;
        private int _currentIndex;

        public Disk SelectedDisk => _selectedDisk;

        public void Initialize(Dictionary<Disk, Sprite> diskOptions, int startIndex)
        {
            _diskOptionsList = new List<KeyValuePair<Disk, Sprite>>(diskOptions);
            _currentIndex = startIndex;
            
            var currentOption = _diskOptionsList[_currentIndex];
            SetSelectedDisk(currentOption.Key, currentOption.Value);
        }

        public void OnButtonClick()
        {
            _currentIndex = (_currentIndex + 1) % _diskOptionsList.Count;

            var currentOption = _diskOptionsList[_currentIndex];
            SetSelectedDisk(currentOption.Key, currentOption.Value);
        }

        private void SetSelectedDisk(Disk disk, Sprite sprite)
        {
            _selectedDisk = disk;
            selectedDiskSprite.sprite = sprite;
        }
    }
}
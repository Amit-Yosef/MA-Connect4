using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using MoonActive.Connect4;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers.UI.StartScreen.SelectSides.Disks
{
    public class DiskButtonsManager : MonoBehaviour
    {
        [SerializedDictionary("Disk", "Disk Sprite")] [SerializeField]
        private SerializedDictionary<Disk, Sprite> diskOptions;

        
        [SerializeField] private List<DiskButton> _buttons; 

        private void Start()
        {
            var diskCount = diskOptions.Count;
            for (int i = 0; i < _buttons.Count; i++)
            {
                var startIndex = i % diskCount;
                _buttons[i].Initialize(diskOptions, startIndex);
            }
        }
        public  List<Disk> GetSelectedDisks()
        {
            var disks = new List<Disk>();
            foreach (var button in _buttons)
            {
                disks.Add(button.SelectedDisk);
            }

            return disks;
        }
    }
}
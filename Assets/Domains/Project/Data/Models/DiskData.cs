using System;
using MoonActive.Connect4;
using UnityEngine;

namespace Project.Data.Models
{
    [Serializable]
    public class DiskData : IHasImage
    {
        public Sprite Sprite => sprite;
        public Disk Disk => disk;
        
        [SerializeField] private Sprite sprite;
        [SerializeField] private Disk disk;
    }
}
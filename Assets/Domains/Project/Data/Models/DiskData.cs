using System;
using MoonActive.Connect4;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class DiskData : IHasImage
    {
        public Sprite PreviewSprite;
        public Disk Disk;
        public Sprite Sprite { get => PreviewSprite; }
        
    }
}
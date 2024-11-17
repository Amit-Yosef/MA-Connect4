using System;
using MoonActive.Connect4;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct DiskData : IHasImage
    {
        public Sprite PreviewSprite;
        public Disk Disk;
        public Sprite Sprite { get => PreviewSprite; }


        public DiskData(Disk disk, Sprite sprite)
        {
            Disk = disk;
            PreviewSprite = sprite;
        }

        public DiskData(Sprite sprite)
        {
            PreviewSprite = sprite;
            Disk = null;
        }
    }
}
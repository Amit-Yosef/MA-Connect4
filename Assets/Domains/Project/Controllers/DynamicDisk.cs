using System;
using Controllers.Players;
using Data;
using Data.FootballApi;
using MoonActive.Connect4;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Controllers
{
    public class DynamicDisk : MonoBehaviour
    {
        [SerializeField] private Disk _disk;
        [SerializeField] Image _image;

        public Disk Disk => _disk;
        
        
        [Inject]
        public void Construct(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        public class Factory : PlaceholderFactory<Sprite, DynamicDisk>
        {
            
        }
        



    }
}
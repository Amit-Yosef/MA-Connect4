using System;
using Controllers.Players;
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
        public void Construct(Team team, Sprite logoSprite)
        {
            name = team.Name;
            _image.sprite = logoSprite;
        }

        private void OnEnable()
        {
            gameObject.SetActive(false);
        }

        public class Factory : PlaceholderFactory<Team, Sprite, DynamicDisk>
        {
            
        }
        



    }
}
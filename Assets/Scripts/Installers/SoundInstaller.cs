using AYellowpaper.SerializedCollections;
using Controllers.UI.StartScreen.SelectSides;
using Data;
using Managers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SoundInstaller : MonoInstaller
    {
        [SerializedDictionary("Key", "Sound")] [SerializeField]
        private SerializedDictionary<SoundType, AudioClip> Sounds;
        
        [SerializeField] private AudioSource audioSource;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SoundSystem>().AsSingle().WithArguments(Sounds, audioSource);
        }
    }
}
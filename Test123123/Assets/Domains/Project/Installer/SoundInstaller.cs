using AYellowpaper.SerializedCollections;
using Project.Data.Models;
using Project.Systems;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Project.Installer
{
    public class SoundInstaller : MonoInstaller
    {
        [SerializedDictionary("Key", "Sound")] [SerializeField]
        private SerializedDictionary<SoundType, AudioClip> sounds;
        
        [SerializeField] private AudioSource audioSource;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SoundSystem>().AsSingle().WithArguments(sounds, audioSource);
        }
    }
}
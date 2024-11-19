using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;
using Project.Data.Models;
using Project.Utils.ObjectPooling;

namespace Project.Systems
{
    public class SoundSystem : IInitializable, IDisposable
    {
        [Inject] private Dictionary<SoundType, AudioClip> _sounds;
        [Inject] private AudioSource _audioSourcePrefab;

        private GenericObjectPool<AudioSource> _audioSourcePool;
        private CancellationTokenSource _cancellationTokenSource;
        public float Volume { get; set; }

        public void Initialize()
        {
            Volume = 1;
            _cancellationTokenSource = new CancellationTokenSource();
            _audioSourcePool = new GenericObjectPool<AudioSource>(_audioSourcePrefab, "AudioSourcePool", dontDestroyOnLoad: true);
        }

        public void PlaySound(SoundType soundType, bool loop = false)
        {
            if (_sounds.TryGetValue(soundType, out var clip))
            {
                var audioSource = _audioSourcePool.GetObject();
                audioSource.clip = clip;
                audioSource.volume = Volume;
                audioSource.loop = loop;
                audioSource.Play();

                if (!loop)
                {
                    ReturnToPoolAfterPlaybackAsync(audioSource, _cancellationTokenSource.Token).Forget();
                }
            }
            else
            {
                Debug.LogWarning($"Sound clip not found for SoundType: {soundType}");
            }
        }

        private async UniTaskVoid ReturnToPoolAfterPlaybackAsync(AudioSource audioSource, CancellationToken cancellationToken)
        {
            try
            {
                await UniTask.WaitUntil(() => !audioSource.isPlaying, cancellationToken: cancellationToken);
                _audioSourcePool.ReturnObject(audioSource);
            }
            catch (OperationCanceledException)
            {
            }
        }
        
        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        public void SetVolume(float value)
        {
            Volume = value;
        }
    }
}

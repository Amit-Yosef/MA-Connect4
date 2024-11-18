using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Domains.DiskSources;
using UnityEngine;
using Zenject;

namespace Data
{
    public class DiskProvidersConfiguration : IInitializable
    {
        public event Action<DefaultGameModeDiskSource> DefaultGameModeProviderChanged;

        private DefaultGameModeDiskSource _defaultGameModeDiskSource = DefaultGameModeDiskSource.MoonActive;
        
        public event Action<FixtureGameModeDiskSource> FixtureGameModeProviderChanged;

        private FixtureGameModeDiskSource _fixtureGameModeDiskSource = FixtureGameModeDiskSource.Football;
        
        public void Initialize()
        {
            DefaultGameModeProviderChanged?.Invoke(_defaultGameModeDiskSource);
            FixtureGameModeProviderChanged?.Invoke(_fixtureGameModeDiskSource);

        }
        
        public DefaultGameModeDiskSource defaultGameModeDiskSource
        {
            get => _defaultGameModeDiskSource;
            set
            {
                if (_defaultGameModeDiskSource != value)
                {
                    _defaultGameModeDiskSource = value;
                    DefaultGameModeProviderChanged?.Invoke(value);
                }
            }
        }
        public FixtureGameModeDiskSource fixtureGameModeDiskSource
        {
            get => _fixtureGameModeDiskSource;
            set
            {
                if (_fixtureGameModeDiskSource != value)
                {
                    _fixtureGameModeDiskSource = value;
                    FixtureGameModeProviderChanged?.Invoke(value);
                }
            }
        }
    }
    public enum DefaultGameModeDiskSource
    {
        Football,
        MoonActive,
    }
    
    public enum FixtureGameModeDiskSource
    {
        Football,
        Basketball
    }
}

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
        public event Func<DefaultGameDiskSourceType,UniTaskVoid> DefaultGameSourceChanged;

        private DefaultGameDiskSourceType _defaultGameDiskSourceType = DefaultGameDiskSourceType.MoonActive;
        
        public event Func<FixtureGameDiskSourceType,UniTaskVoid> FixtureGameSourceChanged;

        private FixtureGameDiskSourceType _fixtureGameDiskSourceType = FixtureGameDiskSourceType.Basketball;
        
        public void Initialize()
        {
            DefaultGameSourceChanged?.Invoke(_defaultGameDiskSourceType);
            FixtureGameSourceChanged?.Invoke(_fixtureGameDiskSourceType);

        }
        
        public DefaultGameDiskSourceType defaultGameDiskSourceType
        {
            get => _defaultGameDiskSourceType;
            set
            {
                if (_defaultGameDiskSourceType != value)
                {
                    _defaultGameDiskSourceType = value;
                    DefaultGameSourceChanged?.Invoke(value);
                }
            }
        }
        public FixtureGameDiskSourceType fixtureGameDiskSourceType
        {
            get => _fixtureGameDiskSourceType;
            set
            {
                if (_fixtureGameDiskSourceType != value)
                {
                    _fixtureGameDiskSourceType = value;
                    FixtureGameSourceChanged?.Invoke(value);
                }
            }
        }
    }
    public enum DefaultGameDiskSourceType
    {
        MoonActive,
        Food
    }
    
    public enum FixtureGameDiskSourceType
    {
        Football,
        Basketball,
    }
}

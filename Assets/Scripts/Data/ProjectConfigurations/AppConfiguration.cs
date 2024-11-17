using System;
using Cysharp.Threading.Tasks;
using Data.FootballApi;
using UnityEngine;

namespace Data
{
    public class AppConfiguration
    {
        public event Action<DiskProviderMode> OnPlayersConfigurationModeChanged;

        private DiskProviderMode _diskProviderMode = DiskProviderMode.Normal;

        public DiskProviderMode diskProviderMode
        {
            get => _diskProviderMode;
            set
            {
                if (_diskProviderMode != value)
                {
                    _diskProviderMode = value;
                    OnPlayersConfigurationModeChanged?.Invoke(value);
                }
            }
        }
    }

    public enum DiskProviderMode
    {
        Football,
        Normal
    }
}
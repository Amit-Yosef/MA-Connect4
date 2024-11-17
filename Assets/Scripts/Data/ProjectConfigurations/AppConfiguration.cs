using System;
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
                    Debug.Log("Invoking OnPlayersConfigurationModeChanged");
                    OnPlayersConfigurationModeChanged?.Invoke(value); //when it resces here the event is null somehow
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
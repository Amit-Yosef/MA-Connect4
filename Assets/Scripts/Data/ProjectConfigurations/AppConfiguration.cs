using System;
using Data.FootballApi;
using UnityEngine;

namespace Data
{
    public class AppConfiguration
    {
        public event Action<PlayersConfigurationMode> OnPlayersConfigurationModeChanged;

        private PlayersConfigurationMode _playersConfigurationMode = PlayersConfigurationMode.Normal;

        public PlayersConfigurationMode PlayersConfigurationMode
        {
            get => _playersConfigurationMode;
            set
            {
                if (_playersConfigurationMode != value)
                {
                    _playersConfigurationMode = value;
                    Debug.Log("Invoking OnPlayersConfigurationModeChanged");
                    OnPlayersConfigurationModeChanged?.Invoke(value); //when it resces here the event is null somehow
                }
            }
        }
    }

    public enum PlayersConfigurationMode
    {
        Football,
        Normal
    }
}
using System;
using Cysharp.Threading.Tasks;
using Data.FootballApi;
using UnityEngine;

namespace Data
{
    public class DiskDataSourceConfig
    {
        public event Action<DiskDataSource> OnPlayersConfigurationModeChanged;

        private DiskDataSource _diskDataSource = DiskDataSource.Normal;

        public DiskDataSource diskDataSource
        {
            get => _diskDataSource;
            set
            {
                if (_diskDataSource != value)
                {
                    _diskDataSource = value;
                    OnPlayersConfigurationModeChanged?.Invoke(value);
                }
            }
        }
    }

    public enum DiskDataSource
    {
        Football,
        Normal,
        RandomPeople
    }
}
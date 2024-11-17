namespace Data
{
    using System.Collections.Generic;
    using UnityEngine;
    using Controllers;

    public struct PlayersViewRequest
    {
        public List<DiskData> DiskOptions { get; }
        public List<PlayerTurnStrategyData> StrategyOptions { get; }
        public RectTransform ParentTransform { get; }
        public int PlayersCount { get; }
        public bool IsStrategyInBigBox { get; }

        private PlayersViewRequest(
            List<DiskData> diskOptions,
            List<PlayerTurnStrategyData> strategyOptions,
            RectTransform parentTransform,
            int playersCount,
            bool isStrategyInBigBox)
        {
            DiskOptions = diskOptions;
            StrategyOptions = strategyOptions;
            ParentTransform = parentTransform;
            PlayersCount = playersCount;
            IsStrategyInBigBox = isStrategyInBigBox;
        }

        public class Builder
        {
            private List<DiskData> _diskOptions = new();
            private List<PlayerTurnStrategyData> _strategyOptions = new();
            private RectTransform _parentTransform;
            private int _playersCount;
            private bool _isStrategyInBigBox = true; // Default value

            public Builder SetDiskOptions(List<DiskData> diskOptions)
            {
                _diskOptions = diskOptions;
                return this;
            }

            public Builder SetStrategyOptions(List<PlayerTurnStrategyData> strategyOptions)
            {
                _strategyOptions = strategyOptions;
                return this;
            }

            public Builder SetParentTransform(RectTransform parentTransform)
            {
                _parentTransform = parentTransform;
                return this;
            }

            public Builder SetPlayersCount(int playersCount)
            {
                _playersCount = playersCount;
                return this;
            }

            public Builder SetIsStrategyInBigBox(bool isStrategyInBigBox)
            {
                _isStrategyInBigBox = isStrategyInBigBox;
                return this;
            }

            public PlayersViewRequest Build()
            {
                return new PlayersViewRequest(
                    _diskOptions,
                    _strategyOptions,
                    _parentTransform,
                    _playersCount,
                    _isStrategyInBigBox
                );
            }
        }
    }
}

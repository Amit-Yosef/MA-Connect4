using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace Data
{
    public struct PlayerCreationViewConfig
    {
        public List<PlayerTurnStrategyData> turnStrategyOptions { get; }
        public List<DiskData> diskOptions { get; }
        public bool isTurnStrategyBigButton { get; }
        public RectTransform rectTransformParent { get; }

        private PlayerCreationViewConfig(
            List<PlayerTurnStrategyData> turnStrategyOptions,
            List<DiskData> diskOptions,
            bool isTurnStrategyBigButton,
            RectTransform rectTransformParent)
        {
            this.turnStrategyOptions = turnStrategyOptions;
            this.diskOptions = diskOptions;
            this.isTurnStrategyBigButton = isTurnStrategyBigButton;
            this.rectTransformParent = rectTransformParent;
        }

        public class Builder
        {
            private List<PlayerTurnStrategyData> _turnStrategyOptions;
            private List<DiskData> _diskOptions = new();
            private bool _isTurnStrategyBigButton = true;
            private RectTransform _rectTransformParent;

            public Builder SetTurnStrategyService(List<PlayerTurnStrategyData> service)
            {
                _turnStrategyOptions = service;
                return this;
            }

            public Builder SetDiskOptions(List<DiskData> options)
            {
                _diskOptions = options;
                return this;
            }
            public Builder SetDiskOptions(DiskData option)
            {
                _diskOptions = new List<DiskData>(){option};
                return this;
            }
            public Builder SetIsTurnStrategyBigButton(bool isBigButton)
            {
                _isTurnStrategyBigButton = isBigButton;
                return this;
            }

            public Builder SetRectTransformParent(RectTransform rectTransformParent)
            {
                _rectTransformParent = rectTransformParent;
                return this;
            }

            public PlayerCreationViewConfig Build()
            {
                return new PlayerCreationViewConfig(
                    _turnStrategyOptions,
                    _diskOptions,
                    _isTurnStrategyBigButton,
                    _rectTransformParent
                );
            }
            
        }
    }
}

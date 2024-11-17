using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public struct ItemSwitcherButtonConfig<T>
    {
        public int StartIndex => 0;
        public bool CanSwitch { get; }
        public List<T> Options { get; }
        public RectTransform RectTransform { get; }

        private ItemSwitcherButtonConfig(bool canSwitch, List<T> options, RectTransform rectTransform)
        {
            CanSwitch = canSwitch;
            Options = options;
            RectTransform = rectTransform;
        }

        public class Builder
        {
            private bool _canSwitch = true;
            private readonly List<T> _options = new List<T>();
            private RectTransform _rectTransform;

            public Builder SetCanSwitch(bool canSwitch)
            {
                _canSwitch = canSwitch;
                return this;
            }

            public Builder AddOptions(IEnumerable<T> options)
            {
                _options.AddRange(options);
                return this;
            }

            public Builder AddOption(T option)
            {
                _options.Add(option);
                return this;
            }

            public Builder SetRectTransform(RectTransform rectTransform)
            {
                _rectTransform = rectTransform;
                return this;
            }

            public ItemSwitcherButtonConfig<T> Build()
            {
                return new ItemSwitcherButtonConfig<T>(_canSwitch, _options, _rectTransform);
            }
        }
    }
}
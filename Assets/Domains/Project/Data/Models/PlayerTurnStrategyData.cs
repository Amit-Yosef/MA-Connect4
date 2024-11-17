using System;
using Controllers;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/Player Turn Strategy")]
    public class PlayerTurnStrategyData : ScriptableObject, IEquatable<PlayerTurnStrategyData> ,IHasImage
    {
        [SerializeField] private Sprite sprite;

        public string Name;
        public PlayerTurnStrategyType Type;
        public Sprite Sprite { get => sprite;}


        public bool Equals(PlayerTurnStrategyData other)
        {
            return Equals(Type, other.Type);
        }

        public override bool Equals(object obj)
        {
            return obj is PlayerTurnStrategyData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type);
        }

    }

    public interface IHasImage
    {
        public Sprite Sprite { get; }
    }
}
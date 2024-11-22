using System;
using StartScreen.DataProviders;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Data.Models
{
    [CreateAssetMenu(menuName = "Data/Player Behavior Data")]
    public class PlayerBehaviorData : ScriptableObject, IHasImage
    {
        public Sprite Sprite => sprite;
        public string Name => behaviourName;
        public PlayerBehaviourType Type => type;
        
        [SerializeField] private Sprite sprite;
        [SerializeField] private string behaviourName;
        [SerializeField] public PlayerBehaviourType type;
    }
}
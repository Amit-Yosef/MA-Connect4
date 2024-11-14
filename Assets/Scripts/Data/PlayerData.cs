using UnityEngine;

namespace Data
{
    public struct PlayerData
    {
        public Sprite Sprite;
        public string Name;

        public PlayerData(string name, Sprite sprite)
        {
            Name = name;
            Sprite = sprite;
        }
    }
}
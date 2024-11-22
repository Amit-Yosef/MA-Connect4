using MoonActive.Connect4;

namespace Project.Data.Models
{
    public struct PlayerData
    {
        
        public PlayerBehaviorData BehaviorData { get; }

        public DiskData DiskData { get; }

        public PlayerData(PlayerBehaviorData behaviorData, DiskData disk)
        {
            BehaviorData = behaviorData;
            DiskData = disk;
        }
    }
}
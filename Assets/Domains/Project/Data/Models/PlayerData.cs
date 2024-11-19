using MoonActive.Connect4;

namespace Project.Data.Models
{
    public struct PlayerData
    {
        public PlayerTurnStrategyData TurnStrategyData;
        public DiskData DiskData;

        public PlayerData(PlayerTurnStrategyData turnStrategyData, DiskData disk)
        {
            TurnStrategyData = turnStrategyData;
            DiskData = disk;
        }
    }
}
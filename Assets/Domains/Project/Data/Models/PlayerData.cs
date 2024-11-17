using MoonActive.Connect4;

namespace Data
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
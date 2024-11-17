using MoonActive.Connect4;

namespace Data
{
    public struct PlayerData
    {
        public PlayerTurnStrategyData TurnStrategyData;
        public Disk Disk;

        public PlayerData(PlayerTurnStrategyData turnStrategyData, Disk disk)
        {
            TurnStrategyData = turnStrategyData;
            Disk = disk;
        }
    }
}
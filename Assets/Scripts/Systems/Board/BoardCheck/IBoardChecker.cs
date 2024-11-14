using MoonActive.Connect4;

namespace Managers
{
    public interface  IBoardChecker
    {
        public BoardCheckResult Check(IDisk[,] board);
    }

    public struct BoardCheckResult
    {
        public BoardCheckResultType Type;
        public IDisk Winner;
    }

    public enum BoardCheckResultType
    {
        Win,
        Draw,
        OnGoing
    }
}
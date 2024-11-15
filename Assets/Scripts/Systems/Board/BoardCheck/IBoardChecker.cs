using System;
using MoonActive.Connect4;

namespace Managers
{
    public interface  IBoardChecker
    {
        public event Action<BoardCheckResult> OnWinOrDraw;

        public void Check(Disk[,] board);
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
using System;
using Data;
using MoonActive.Connect4;

namespace Managers
{
    public interface  IBoardChecker
    {
        public event Action<BoardCheckResult> OnWinOrDraw;

        public void Check(DiskData[,] board);
    }

    public struct BoardCheckResult
    {
        public BoardCheckResultType Type;
        public DiskData Winner;
    }

    public enum BoardCheckResultType
    {
        Win,
        Draw,
        OnGoing
    }
}
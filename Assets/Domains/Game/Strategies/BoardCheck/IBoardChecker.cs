using System;
using Project.Data.Models;

namespace Game.Strategies.BoardCheck
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
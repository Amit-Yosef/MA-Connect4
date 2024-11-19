using System;
using Project.Data.Models;

namespace Game.Strategies.BoardCheck
{
    public class BoardChecker : IBoardChecker
    {
        public const int WinningCount = 4;
        
        public event Action<BoardCheckResult> OnWinOrDraw;
          public void Check(DiskData[,] board)
        {
            int rows = board.GetLength(0);
            int cols = board.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    DiskData currentDiskData = board[row, col];

                    if (currentDiskData == null)
                        continue;

                    if (CheckDirection(board, row, col, 0, 1, currentDiskData) ||
                        CheckDirection(board, row, col, 1, 0, currentDiskData) ||
                        CheckDirection(board, row, col, 1, 1, currentDiskData) ||
                        CheckDirection(board, row, col, -1, 1, currentDiskData))
                    {
                        var winResult = new BoardCheckResult
                        {
                            Type = BoardCheckResultType.Win,
                            Winner = currentDiskData
                        };
                        
                        OnWinOrDraw?.Invoke(winResult);
                        return;
                    }
                }
            }

            bool isDraw = true;
            for (int col = 0; col < cols; col++)
            {
                if (board[rows - 1, col] == null)
                {
                    isDraw = false;
                    break;
                }
            }

            var result = isDraw
                ? new BoardCheckResult { Type = BoardCheckResultType.Draw, Winner = null }
                : new BoardCheckResult { Type = BoardCheckResultType.OnGoing, Winner = null };

            if (result.Type == BoardCheckResultType.Draw)
            {
                OnWinOrDraw?.Invoke(result);
            }

        }

        private bool CheckDirection(DiskData[,] board, int row, int col, int rowDir, int colDir, DiskData disk)
        {
            int count = 0;
            int rows = board.GetLength(0);
            int cols = board.GetLength(1);

            for (int i = 0; i < WinningCount; i++)
            {
                int newRow = row + i * rowDir;
                int newCol = col + i * colDir;

                if (newRow < 0 || newRow >= rows || newCol < 0 || newCol >= cols)
                    break;

                if (board[newRow, newCol] != disk)
                    break;

                count++;
            }

            return count == WinningCount;
        }
        
    }
}
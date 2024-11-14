using MoonActive.Connect4;

namespace Managers
{
    public class BoardChecker : IBoardChecker
    {
        private const int WinningCount = 4;
        
        public BoardCheckResult Check(IDisk[,] board)
        {
             int rows = board.GetLength(0);
            int cols = board.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    IDisk currentDisk = board[row, col];

                    if (currentDisk == null)
                        continue;

                    if (CheckDirection(board, row, col, 0, 1, currentDisk))
                        return new BoardCheckResult { Type = BoardCheckResultType.Win, Winner = currentDisk };

                    if (CheckDirection(board, row, col, 1, 0, currentDisk))
                        return new BoardCheckResult { Type = BoardCheckResultType.Win, Winner = currentDisk };

                    if (CheckDirection(board, row, col, 1, 1, currentDisk))
                        return new BoardCheckResult { Type = BoardCheckResultType.Win, Winner = currentDisk };

                    if (CheckDirection(board, row, col, -1, 1, currentDisk))
                        return new BoardCheckResult { Type = BoardCheckResultType.Win, Winner = currentDisk };
                }
            }

            bool isDraw = true;
            for (int col = 0; col < cols; col++)
            {
                if (board[0, col] == null)
                {
                    isDraw = false;
                    break;
                }
            }

            return isDraw
                ? new BoardCheckResult { Type = BoardCheckResultType.Draw, Winner = null }
                : new BoardCheckResult { Type = BoardCheckResultType.OnGoing, Winner = null };
        }

        private bool CheckDirection(IDisk[,] board, int row, int col, int rowDir, int colDir, IDisk disk)
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
using Game.Strategies.BoardCheck;
using NUnit.Framework;
using Moq;
using Project.Data.Models;

namespace UnitTests
{
    [TestFixture]
    public class BoardCheckerTests
    {
        private const int Rows = 6;
        private const int Cols = 7;
        private const int WinningCount = BoardChecker.WinningCount;
        private BoardChecker _boardChecker;
        private bool _eventInvoked;
        private BoardCheckResult _lastResult;
        private (DiskData disk1, DiskData disk2) _disks;

        [SetUp]
        public void SetUp()
        {
            _disks.disk1 = new DiskData();
            _disks.disk2 = new DiskData();

            _boardChecker = new BoardChecker();
            _eventInvoked = false;
            _lastResult = new BoardCheckResult();

            _boardChecker.OnWinOrDraw += result =>
            {
                _eventInvoked = true;
                _lastResult = result;
            };
        }

        [Test]
        public void Check_HorizontalWin_TriggersWinEvent()
        {
            var board = new DiskData[Rows, Cols];
            var playerDiskData = _disks.disk1;
            for (int col = 0; col < WinningCount; col++) board[0, col] = playerDiskData;
            _boardChecker.Check(board);

            Assert.IsTrue(_eventInvoked);
            Assert.AreEqual(BoardCheckResultType.Win, _lastResult.Type);
            Assert.AreEqual(playerDiskData, _lastResult.Winner);
        }

        [Test]
        public void Check_VerticalWin_TriggersWinEvent()
        {
            var board = new DiskData[Rows, Cols];
            var playerDiskData = _disks.disk1;
            for (int row = 0; row < WinningCount; row++) board[row, 0] = playerDiskData;

            _boardChecker.Check(board);

            Assert.IsTrue(_eventInvoked);
            Assert.AreEqual(BoardCheckResultType.Win, _lastResult.Type);
            Assert.AreEqual(playerDiskData, _lastResult.Winner);
        }

        [Test]
        public void Check_DiagonalWin_TriggersWinEvent()
        {
            var board = new DiskData[Rows, Cols];
            var playerDiskData = _disks.disk1;
            for (int i = 0; i < WinningCount; i++) board[i, i] = playerDiskData;

            _boardChecker.Check(board);

            Assert.IsTrue(_eventInvoked);
            Assert.AreEqual(BoardCheckResultType.Win, _lastResult.Type);
            Assert.AreEqual(playerDiskData, _lastResult.Winner);
        }

        [Test]
        public void Check_AntiDiagonalWin_TriggersWinEvent()
        {
            var board = new DiskData[Rows, Cols];
            var playerDiskData = _disks.disk1;
            for (int i = 0; i < WinningCount; i++) board[i, 6 - i] = playerDiskData;

            _boardChecker.Check(board);

            Assert.IsTrue(_eventInvoked);
            Assert.AreEqual(BoardCheckResultType.Win, _lastResult.Type);
            Assert.AreEqual(playerDiskData, _lastResult.Winner);
        }

        [Test]
        public void Check_Draw_TriggersDrawEvent()
        {
            var board = new DiskData[Rows, Cols];
            FillBoardNonWin(board);

            _boardChecker.Check(board);

            Assert.IsTrue(_eventInvoked);
            Assert.AreEqual(BoardCheckResultType.Draw, _lastResult.Type);
            Assert.IsNull(_lastResult.Winner);
        }

        [Test]
        public void Check_OnGoingGame_DoesNotTriggerWinOrDrawEvent()
        {
            var board = new DiskData[Rows, Cols];
            FillBoardNonWin(board, Cols - 1);

            _boardChecker.Check(board);

            Assert.IsFalse(_eventInvoked);
        }

        private void FillBoardNonWin(DiskData[,] board)
        {
            FillBoardNonWin(board, board.GetLength(1));
        }

        private void FillBoardNonWin(DiskData[,] board, int columnLimit)
        {
            int rowLimit = board.GetLength(0);

            for (int row = 0; row < rowLimit; row = row + 2)
            {
                for (int col = 0; col < columnLimit && col < board.GetLength(1); col++)
                {
                    if (board[row, col] != null) continue;

                    var disk = (row % 4 == 0)
                        ? ((col % 2 == 0) ? _disks.disk1 : _disks.disk2)
                        : ((col % 2 == 0) ? _disks.disk2 : _disks.disk1);

                    board[row, col] = disk;

                    if (row + 1 < rowLimit)
                    {
                        board[row + 1, col] = disk;
                    }
                }
            }
        }
    }
}
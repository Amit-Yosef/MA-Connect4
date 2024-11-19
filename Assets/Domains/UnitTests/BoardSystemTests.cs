using System;
using NUnit.Framework;
using NSubstitute;
using Zenject;
using Data;
using MoonActive.Connect4;

namespace Managers.Tests
{
    [TestFixture]
    public class BoardSystemTests : ZenjectUnitTestFixture
    {
        private BoardSystem _boardSystem;
        private IBoardChecker _mockBoardChecker;
        private Func<Disk, int, int, IDisk> _mockDiskSpawner;
        private PopUpSystem _mockPopupSystem;
        private IDisk _mockDiskInstance;

        [SetUp]
        public void SetUp()
        {
            _mockBoardChecker = Substitute.For<IBoardChecker>();
            _mockPopupSystem = Substitute.For<PopUpSystem>();
            _mockDiskInstance = Substitute.For<IDisk>();

            _mockDiskSpawner = Substitute.For<Func<Disk, int, int, IDisk>>();
            _mockDiskSpawner.Invoke(Arg.Any<Disk>(), Arg.Any<int>(), Arg.Any<int>())
                .Returns(_mockDiskInstance);

            _mockDiskInstance
                .When(x => x.StoppedFalling += Arg.Any<Action>())
                .Do(callInfo => 
                {
                    var handler = callInfo.Arg<Action>();
                    handler?.Invoke();
                });

            Container.Bind<IBoardChecker>().FromInstance(_mockBoardChecker);
            Container.Bind<Func<Disk, int, int, IDisk>>().FromInstance(_mockDiskSpawner);
            Container.Bind<PopUpSystem>().FromInstance(_mockPopupSystem);

            Container.Bind<BoardSystem>().AsSingle().NonLazy();

            _boardSystem = Container.Resolve<BoardSystem>();
        }

        [Test]
        public void Initialize_BoardCreatedWithCorrectDimensions()
        {
            Assert.AreEqual(GameConfiguration.VERTICAL_SIZE, _boardSystem.Board.GetLength(0));
            Assert.AreEqual(GameConfiguration.HORIZONTAL_SIZE, _boardSystem.Board.GetLength(1));
        }

        [Test]
        public void IsColumnFull_EmptyColumn_ReturnsFalse()
        {
            int column = 0;

            bool result = _boardSystem.IsColumnFull(column);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsColumnFull_FullColumn_ReturnsTrue()
        {
            int column = 0;
            for (int row = 0; row < GameConfiguration.VERTICAL_SIZE; row++)
            {
                _boardSystem.Board[row, column] = new DiskData();
            }

            bool result = _boardSystem.IsColumnFull(column);

            Assert.IsTrue(result);
        }
    }
}
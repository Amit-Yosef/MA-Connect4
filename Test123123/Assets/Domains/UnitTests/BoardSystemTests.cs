using System;
using System.Collections.Generic;
using Game.Strategies.BoardCheck;
using Game.Systems;
using NUnit.Framework;
using Moq;
using Zenject;
using MoonActive.Connect4;
using Project.Data.Models;
using Project.Systems;
using UnityEngine;

namespace UnitTests
{
    [TestFixture]
    public class BoardSystemTests : ZenjectUnitTestFixture
    {
        private BoardSystem _boardSystem;
        private Mock<IBoardChecker> _mockBoardChecker;
        private Mock<Func<Disk, int, int, IDisk>> _mockDiskSpawner;
        private Mock<PopUpSystem> _mockPopupSystem;
        private Mock<SoundSystem> _mockSoundSystem;
        private Mock<IDisk> _mockDiskInstance;

        [SetUp]
        public void SetUp()
        {
            _mockBoardChecker = new Mock<IBoardChecker>();
            _mockPopupSystem = new Mock<PopUpSystem>();
            _mockSoundSystem = new Mock<SoundSystem>();
            _mockDiskInstance = new Mock<IDisk>();

            _mockDiskSpawner = new Mock<Func<Disk, int, int, IDisk>>();
            _mockDiskSpawner
                .Setup(spawner => spawner(It.IsAny<Disk>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(_mockDiskInstance.Object);

            _mockDiskInstance
                .SetupAdd(disk => disk.StoppedFalling += It.IsAny<Action>())
                .Callback<Action>(handler => handler?.Invoke());

            Container.Bind<IBoardChecker>().FromInstance(_mockBoardChecker.Object);
            Container.Bind<Func<Disk, int, int, IDisk>>().FromInstance(_mockDiskSpawner.Object);
            Container.Bind<PopUpSystem>().FromInstance(_mockPopupSystem.Object);
            Container.Bind<SoundSystem>().FromInstance(_mockSoundSystem.Object);

            Container.Bind<BoardSystem>().AsSingle();

            _boardSystem = Container.Resolve<BoardSystem>();
            _boardSystem.Initialize();
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

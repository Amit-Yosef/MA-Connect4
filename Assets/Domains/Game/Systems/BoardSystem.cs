using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Data;
using UnityEngine;
using MoonActive.Connect4;
using Utils;
using Utils.ExtensionMethods;
using Zenject;
using Random = UnityEngine.Random;

namespace Managers
{
    public class BoardSystem : IInitializable
    {
        [Inject] private Func<Disk,int,int,IDisk> _spawnDiskOnView;
        [Inject] private IBoardChecker _checker;
        [Inject] private PopUpSystem _popupSystem;

        private readonly int _rows = GameConfiguration.VERTICAL_SIZE;
        private readonly int _columns = GameConfiguration.HORIZONTAL_SIZE;
        public DiskData[,] Board { get; private set; }

        [Inject]
        public void Initialize()
        {
            Board = new DiskData[_rows, _columns];
        }

        public async UniTask AddPiece(int column, DiskData diskData)
        {
            if (IsColumnFull(column))
            {
                Debug.LogWarning($"Column {column} is full.");
                return;
            }

            int row = GetLowestAvailableRow(column);
            var diskInstance = _spawnDiskOnView(diskData.Disk, column, row);

            await diskInstance.WaitForDiskToStopFalling();
            Board[row, column] = diskData;
            _checker.Check(Board);
        }

        private int GetLowestAvailableRow(int column)
        {
            for (int row = 0; row < _rows; row++)
            {
                if (Board[row, column] == null)
                {
                    return row;
                }
            }

            Debug.LogError($"Column {column} is full.");
            return -1;
        }

        public bool IsColumnFull(int column)
        {
            return Board[_rows - 1, column] != null;
        }
    }
}
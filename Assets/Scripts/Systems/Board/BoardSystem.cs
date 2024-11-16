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
        [Inject] private ConnectGameGrid _grid;
        [Inject] private IBoardChecker _checker;
        [Inject] private PopUpSystem _popupSystem;

        private readonly int _rows = GameConfiguration.HORIZONTAL_SIZE;
        private readonly int _columns = GameConfiguration.HORIZONTAL_SIZE;
        private Disk[,] _board;
        
        
        [Inject]
        public void Initialize()
        {
            _board = new Disk[_rows, _rows];
        }

        public async UniTask AddPiece(int column, Disk diskPrefab)
        {
            if (IsFull(column))
            {
                Debug.LogWarning($"Column {column} is full.");
                return;
            }

            int row = GetLowestAvailableRow(column);
            var diskInstance = _grid.Spawn(diskPrefab, column, row);

            await diskInstance.WaitForDiskToStopFalling();
            _board[row, column] = diskPrefab;
            _checker.Check(_board);
        }

        public int GetRandomValidColumn() //ToDo: Move This From Here
        {
            var validColumns = Enumerable.Range(0, _columns).Where(column => !IsFull(column)).ToList();

            if (validColumns.Count == 0)
            {
                Debug.LogError("All columns are full.");
                return -1;
            }

            return validColumns[Random.Range(0, validColumns.Count)];
        }

        private int GetLowestAvailableRow(int column)
        {
            for (int row = 0; row < _rows; row++)
            {
                if (_board[row, column] == null)
                {
                    return row;
                }
            }

            Debug.LogError($"Column {column} is full.");
            return -1;
        }

        private bool IsFull(int column)
        {
            return _board[_rows - 1, column] != null;
        }
    }
}
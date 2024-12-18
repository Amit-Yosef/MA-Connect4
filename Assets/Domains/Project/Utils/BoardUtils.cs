using System.Linq;
using Game.Systems;
using UnityEngine;

namespace Project.Utils
{
    public static class BoardUtils
    {
        public static int GetRandomValidColumn(BoardSystem boardSystem)
        {
            var colums = boardSystem.Board.GetLength(1);
            var validColumns = Enumerable.Range(0, colums).Where(column => !boardSystem.IsColumnFull(column)).ToList();

            if (validColumns.Count == 0)
            {
                Debug.LogError("All columns are full.");
                return -1;
            }

            return validColumns[Random.Range(0, validColumns.Count)];
        }       

    }
}
using System.Linq;
using Managers;
using UnityEngine;

namespace Utils
{
    public static class BoardUtills
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
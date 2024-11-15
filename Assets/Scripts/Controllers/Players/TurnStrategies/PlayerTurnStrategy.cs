using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Data;
using Managers;
using MoonActive.Connect4;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Controllers
{
    public abstract class PlayerTurnStrategy : IPlayerTurnStrategy
    {
        [Inject] protected BoardSystem BoardSystem;


        public async UniTask DoTurn(Disk disk, CancellationTokenSource cts)
        {
            try
            {
                var column = await SelectColumn(cts);
                await BoardSystem.AddPiece(column, disk);
            }
            catch (OperationCanceledException)
            {
               Debug.Log("Turn was canceled.");
            }
        }


        protected abstract UniTask<int> SelectColumn(CancellationTokenSource cts);
        
        public abstract PlayerTurnStrategyData GetPlayerData();

    }

    public interface IPlayerTurnStrategy
    {
        UniTask DoTurn(Disk disk, CancellationTokenSource cts);
        PlayerTurnStrategyData GetPlayerData();
    }
}
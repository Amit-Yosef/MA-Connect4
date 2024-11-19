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
    public abstract class PlayerTurn : IPlayerTurnStrategy
    {
        [Inject] protected BoardSystem BoardSystem;


        public async UniTask DoTurn(DiskData disk, CancellationToken cancellationToken)
        {
            try
            {
                var column = await SelectColumn(cancellationToken);
                await BoardSystem.AddPiece(column, disk);
            }
            catch (OperationCanceledException)
            {
               Debug.Log("Turn was canceled.");
            }
        }


        protected abstract UniTask<int> SelectColumn(CancellationToken cancellationToken);
        
        public abstract PlayerTurnStrategyData GetPlayerData();

    }

    public interface IPlayerTurnStrategy
    {
        UniTask DoTurn(DiskData disk, CancellationToken cancellationToken);
        PlayerTurnStrategyData GetPlayerData();
    }
}
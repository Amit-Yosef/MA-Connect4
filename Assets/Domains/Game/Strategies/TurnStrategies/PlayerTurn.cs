using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Systems;
using Project.Data.Models;
using UnityEngine;
using Zenject;

namespace Game.Strategies.TurnStrategies
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
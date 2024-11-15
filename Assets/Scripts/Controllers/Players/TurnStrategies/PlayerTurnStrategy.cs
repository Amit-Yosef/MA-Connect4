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


        public async UniTask DoTurn(Disk disk)
        {
            var column = await SelectColumn();
            await BoardSystem.AddPiece(column, disk);
        }

        protected abstract UniTask<int> SelectColumn();
        
        public abstract PlayerTurnStrategyData GetPlayerData();

    }

    public interface IPlayerTurnStrategy
    {
        UniTask DoTurn(Disk disk);
        PlayerTurnStrategyData GetPlayerData();
    }
}
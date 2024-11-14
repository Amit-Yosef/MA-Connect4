using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Data;
using Managers;
using MoonActive.Connect4;
using Services;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Controllers
{
    public abstract class BasePlayer
    {
        [Inject] protected BoardSystem BoardSystem;
        [Inject] protected PlayerDataProviderService _dataProvider;

        protected Disk DiskPrefab;

        public async UniTask DoTurn()
        {
            var column = await SelectColumn();
            await BoardSystem.AddPiece(column, DiskPrefab);
            Debug.Log(GetData().Name);
        }

        protected abstract UniTask<int> SelectColumn();

        private PlayerData GetData()
        {
            return _dataProvider.Get(GetType());
        }
    }


}
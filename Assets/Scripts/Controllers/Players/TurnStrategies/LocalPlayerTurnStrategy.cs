using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Data;
using Managers;
using MoonActive.Connect4;
using Zenject;
using UnityEngine;
using Utils.ExtensionMethods;

namespace Controllers
{
    public class LocalPlayerTurnStrategy : PlayerTurnStrategy
    {
        [Inject] private ConnectGameGrid _grid;
        private PlayerTurnStrategyData _strategyData;
        public override PlayerTurnStrategyData GetPlayerData() => _strategyData;
        
        protected override async UniTask<int> SelectColumn()
        {
            return await _grid.WaitForColumnSelect();
        }
        

        
    }
    
    
}
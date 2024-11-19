using System;
using System.Collections.Generic;
using Controllers;
using Cysharp.Threading.Tasks;
using Data;
using UnityEngine;
using Zenject;

namespace Domains.StartScreen.Services
{
    public class DiskDataProvider  
    {
        [Inject] private List<DiskData> _diskDatas;
        public bool IsDataLoaded => true;


        public List<DiskData> GetDisks()
        {
            return _diskDatas;
        }
    }
}
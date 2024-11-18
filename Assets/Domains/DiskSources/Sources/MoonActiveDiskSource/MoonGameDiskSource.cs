using System;
using System.Collections.Generic;
using Controllers;
using Cysharp.Threading.Tasks;
using Data;
using Domains.DiskSources.Interfaces;
using Domains.DiskSources.Providers;
using UnityEngine;
using Zenject;

namespace Domains.DiskSources.Sources.Football
{
    public class MoonGameDiskSource  : DefaultGameDiskSource
    {
        [Inject] private List<DiskData> _diskDatas;


        protected override DefaultGameDiskSourceType GetDefaultDiskSource() => DefaultGameDiskSourceType.MoonActive;

        protected override UniTask<List<DiskData>> GetDiskDatas() => UniTask.FromResult(_diskDatas);
    }
}
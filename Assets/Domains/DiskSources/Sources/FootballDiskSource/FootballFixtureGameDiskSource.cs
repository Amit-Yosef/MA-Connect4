using System;
using System.Collections.Generic;
using Controllers;
using Controllers.UI.StartScreen.SelectSides;
using Cysharp.Threading.Tasks;
using Data;
using Domains.DiskSources;
using Domains.DiskSources.Data;
using Domains.DiskSources.Interfaces;
using Domains.DiskSources.Providers;
using UnityEngine;
using Zenject;

namespace Domains.DiskSources.Sources.Football
{
    public class FootballFixtureGameDiskSource  : FixtureGameDiskSource
    {
         private FootballApiClient _footballApiClient = new FootballApiClient();

         protected override FixtureGameDiskSourceType GetFixtureDiskSource() => FixtureGameDiskSourceType.Football;

         protected override async UniTask<List<Fixture>> GetFixtures()
         {
             return await UniTask.RunOnThreadPool(() =>
                 _footballApiClient.GetNextMatchesByLeagueIdAsync((int)FootballConsts.League));
         }
    }
}
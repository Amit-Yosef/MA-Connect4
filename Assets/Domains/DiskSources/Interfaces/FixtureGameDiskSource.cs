using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;
using Domains.DiskSources.Data;
using Domains.DiskSources.Providers;
using Domains.DiskSources.Sources.Football;
using Zenject;

namespace Domains.DiskSources.Interfaces
{
    public abstract class FixtureGameDiskSource : IInitializable, IDisposable 
    {
        [Inject] private DiskProvidersConfiguration _diskProvidersConfiguration;
        [Inject] private FixtureGameDiskProvider _fixtureGameDiskProvider;

        private Dictionary<Fixture, List<DiskData>> _cachedDisks;

        protected abstract FixtureGameDiskSourceType GetFixtureDiskSource();

        public void Initialize()
        {
            _diskProvidersConfiguration.FixtureGameSourceChanged += FixtureGameSourceChanged;
        }

        private async UniTaskVoid FixtureGameSourceChanged(FixtureGameDiskSourceType mode)
        {
            if (mode == GetFixtureDiskSource())
            {
                _fixtureGameDiskProvider.Reset();

                if (_cachedDisks != null)
                {
                    _fixtureGameDiskProvider.Populate(_cachedDisks);
                    return;                    
                }
                var fixtures = await GetFixtures();
                await _fixtureGameDiskProvider.Populate(fixtures);

                _cachedDisks = _fixtureGameDiskProvider.GetFixturesDisksDictionary();
            }

        }

    

        protected abstract UniTask<List<Fixture>> GetFixtures();

        public void Dispose()
        {
            _diskProvidersConfiguration.FixtureGameSourceChanged += FixtureGameSourceChanged;

        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Domains.DiskSources;
using Domains.DiskSources.Data;

namespace Data
{
    public struct SelectSidePopupRequest
    {
        public bool IsFixture { get; private set; }
        public Func<Fixture,CancellationToken, UniTask<List<DiskData>>> GetDisksByFixture { get; private set; }
        public Func<List<Fixture>> GetFixtures { get; private set; }
        public Func<List<DiskData>> GetDiskData { get; private set; }
        public Func<bool> IsDataLoaded { get; private set; }

        public class Builder
        {
            private SelectSidePopupRequest _request;

            private Builder() { }

            public static FixtureBuilder CreateFixtureRequest()
            {
                return new FixtureBuilder();
            }

            public static DiskDataBuilder CreateDiskDataRequest()
            {
                return new DiskDataBuilder();
            }

            public class FixtureBuilder
            {
                private SelectSidePopupRequest _request = new SelectSidePopupRequest();

                public FixtureBuilder WithGetFixtures(Func<List<Fixture>> getFixtures)
                {
                    _request.GetFixtures = getFixtures;
                    return this;
                }

                public FixtureBuilder WithGetDisksByFixture(Func<Fixture,CancellationToken, UniTask<List<DiskData>>> getDisksByFixture)
                {
                    _request.GetDisksByFixture = getDisksByFixture;
                    return this;
                }

                public FixtureBuilder WithIsDataLoaded(Func<bool> isDataLoaded)
                {
                    _request.IsDataLoaded = isDataLoaded;
                    return this;
                }

                public SelectSidePopupRequest Build()
                {
                    _request.IsFixture = true;
                    return _request;
                }
            }

            public class DiskDataBuilder
            {
                private SelectSidePopupRequest _request = new SelectSidePopupRequest();

                public DiskDataBuilder WithGetDiskData(Func<List<DiskData>> getDiskData)
                {
                    _request.GetDiskData = getDiskData;
                    return this;
                }

                public DiskDataBuilder WithIsDataLoaded(Func<bool> isDataLoaded)
                {
                    _request.IsDataLoaded = isDataLoaded;
                    return this;
                }

                public SelectSidePopupRequest Build()
                {
                    _request.IsFixture = false;
                    return _request;
                }
            }
        }
    }
}

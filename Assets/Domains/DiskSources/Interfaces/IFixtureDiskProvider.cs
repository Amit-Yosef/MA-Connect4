using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;

namespace Domains.DiskSources.Interfaces
{
    public interface IFixtureDiskProvider<T>
    {
        List<T> GetFixtures();
        UniTask<List<DiskData>> GetDisks(T fixture);
        UniTask Populate(List<T> fixtures);
    }
}
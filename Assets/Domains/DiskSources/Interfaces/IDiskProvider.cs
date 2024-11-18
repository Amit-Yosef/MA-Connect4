using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;

namespace Domains.DiskSources.Interfaces
{
    public interface IDiskProvider<T>
    {
        UniTask<List<DiskData>> GetDisks();
    }
}
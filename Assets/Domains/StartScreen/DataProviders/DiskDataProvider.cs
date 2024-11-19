using System.Collections.Generic;
using Project.Data.Models;
using Zenject;

namespace StartScreen.DataProviders
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
using System;
using Domains.DiskSources;

namespace Domains.DiskSources.Data
{
    public class Fixture 
    {
        public DateTime Date { get; set; }
        public string BackgroundImage { get; set; }
        public FixtureMember Home { get; set; }
        public FixtureMember Away { get; set; }


        public override string ToString() => $"{Date.ToShortDateString()} - {Home} vs {Away}";
    }


}
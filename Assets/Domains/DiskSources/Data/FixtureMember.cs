namespace Domains.DiskSources.Data
{
    public class FixtureMember 
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public int TeamId { get; set; }

        public override string ToString() => Name;
    }
    
}
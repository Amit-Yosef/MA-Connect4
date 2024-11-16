namespace Data.FootballApi
{
    public class Team
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public int TeamId { get; set; }

        public override string ToString() => $"{Name}";
    }
}
namespace Domains.DiskSources.Sources.Football
{
    public static class FootballConsts
    {
        public const string BaseUrl = "https://api-football-v1.p.rapidapi.com/v3/fixtures";
        public const string ApiKey = "14f7473ff1msh312fe2e80fcd13ap1a91b4jsn87c403fed106";
        public const string ApiHost = "api-football-v1.p.rapidapi.com";
        public const FootballLeague League = FootballLeague.IsraeliLeague;

    }
    public enum FootballLeague
    {
        IsraeliLeague = 383,
        PremierLeague = 39,
        LaLiga = 140,
        Bundesliga = 78
    }
}
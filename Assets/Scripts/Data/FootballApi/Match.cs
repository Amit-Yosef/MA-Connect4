using System;

namespace Data.FootballApi
{
    public class Match
    {
        public DateTime Date { get; set; }
        public string LeagueLogoUrl { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }

        public override string ToString() => $"{Date.ToShortDateString()} - {HomeTeam} vs {AwayTeam}";
    }
}
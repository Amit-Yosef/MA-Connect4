using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Data.FootballApi;
using Newtonsoft.Json.Linq;
using Zenject;

namespace Controllers
{
    public class FootballApiFetcher : IInitializable
    {
        private HttpClient _httpClient;
        private const string BaseUrl = "https://api-football-v1.p.rapidapi.com/v3/fixtures";
        private const string ApiKey = "14f7473ff1msh312fe2e80fcd13ap1a91b4jsn87c403fed106";
        private const string ApiHost = "api-football-v1.p.rapidapi.com";

        [Inject]
        public void Initialize()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", ApiKey);
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", ApiHost);

        }

        public async UniTask<List<Match>> GetNextMatchesByLeagueIdAsync(int leagueId, int count = 5)
        {
            var url = $"{BaseUrl}?league={leagueId}&next={count}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return ParseMatches(responseBody);
        }

        private List<Match> ParseMatches(string json)
        {
            var matches = new List<Match>();

            try
            {
                var data = JObject.Parse(json);
                var response = data["response"];

                foreach (var matchData in response)
                {
                    var fixture = matchData["fixture"];
                    var date = fixture["date"].ToObject<DateTime>();

                    var teams = matchData["teams"];
                    var homeTeam = new Team
                    {
                        Name = teams["home"]["name"]?.ToString(), LogoUrl = teams["home"]["logo"]?.ToString()
                    };

                    var awayTeam = new Team
                    {
                        Name = teams["away"]["name"]?.ToString(), LogoUrl = teams["away"]["logo"]?.ToString()
                    };

                    var match = new Match { Date = date, HomeTeam = homeTeam, AwayTeam = awayTeam };

                    matches.Add(match);
                }
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"Failed to parse matches: {ex.Message}");
            }

            return matches;
        }
    }
}
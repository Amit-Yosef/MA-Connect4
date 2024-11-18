using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Domains.DiskSources.Data;
using Newtonsoft.Json.Linq;
using Zenject;

namespace Domains.DiskSources.Sources.Football
{
    public class FootballApiClient
    {
        private HttpClient _httpClient;
        
        public FootballApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", FootballConsts.ApiKey);
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", FootballConsts.ApiHost);

        }

        public async UniTask<List<Fixture>> GetNextMatchesByLeagueIdAsync(int leagueId, int count = 5)
        {
            var url = $"{FootballConsts.BaseUrl}?league={leagueId}&next={count}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return ParseMatches(responseBody);
        }

        private List<Fixture> ParseMatches(string json)
        {
            var matches = new List<Fixture>();

            try
            {
                var data = JObject.Parse(json);
                var response = data["response"];

                foreach (var matchData in response)
                {
                    var fixture = matchData["fixture"];
                    var date = fixture["date"].ToObject<DateTime>();
                    
                    var leagueLogoUrl = matchData["league"]["logo"].ToString();

                    var teams = matchData["teams"];
                    var homeTeam = new FixtureMember
                    {
                        Name = teams["home"]["name"]?.ToString(), LogoUrl = teams["home"]["logo"]?.ToString()
                    };

                    var awayTeam = new FixtureMember
                    {
                        Name = teams["away"]["name"]?.ToString(), LogoUrl = teams["away"]["logo"]?.ToString()
                    };

                    var match = new Fixture { Date = date, Home = homeTeam, Away = awayTeam, BackgroundImage = leagueLogoUrl};

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
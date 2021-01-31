using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BestBet.Models;
using System.Net.Http;

namespace BestBet.Services
{
    public class OddsAPI : OddsAPIInterface
    {
        public OddsAPI()
        {
        }

        private static readonly string API_KEY = "b08ff565ec8d09f66946738f80169f8b";


        public async Task<ObservableCollection<Sport>> getSports()
        {
            string url = $"https://api.the-odds-api.com/v3/sports?apiKey={API_KEY}";

            HttpClient client = new HttpClient();

            HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();

                var json = JsonConvert.DeserializeObject<ListOfSports>(result);

                ObservableCollection<Sport> sports = json.data;

                return sports;
            } else
            {
                return null;
            }
        }

        public async Task<ObservableCollection<Match>> getOdds(string title, string region)
        {
            string url = $"https://api.the-odds-api.com/v3/odds/?sport={title}&region={region}&apiKey={API_KEY}";

            HttpClient client = new HttpClient();

            HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();

                var json = JsonConvert.DeserializeObject<ListOfOdds>(result);

                ObservableCollection<Match> matches = json.data;

                return matches;
            }
            else
            {
                return null;
            }
        }

        //public async Task<ObservableCollection<SpreadsMatch>> getSpreads(string title, string region)
        //{
        //    string url = $"https://api.the-odds-api.com/v3/odds/?sport={title}&region={region}&mkt=spreads&apiKey={API_KEY}";

        //    HttpClient client = new HttpClient();

        //    HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();

        //    if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //    {
        //        var result = await response.Content.ReadAsStringAsync();

        //        var json = JsonConvert.DeserializeObject<AllSpreads>(result);

        //        ObservableCollection<SpreadsMatch> matches = json.data;

        //        return matches;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public async Task<ObservableCollection<Match>> getUpcomingMatches(string title, string region)
        {
            string url = $"https://8k4l5ct991.execute-api.us-east-1.amazonaws.com/dev/odds/upcoming";

            HttpClient client = new HttpClient();

            HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();

                var json = JsonConvert.DeserializeObject<ListOfOdds>(result);

                ObservableCollection<Match> matches = json.data;

                return matches;
            }
            else
            {
                return null;
            }
        }
    }
}

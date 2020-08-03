using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace recommendSongsService.API.Service
{
    public class OpenWeatherMapService
    {
        private readonly WebConfiguration _webConfiguration;
        private static HttpClient client;
        private static string spotifyToken;

        public OpenWeatherMapService(IOptionsMonitor<WebConfiguration> webConfiguration)
        {
            _webConfiguration = webConfiguration.CurrentValue;
            client = new HttpClient();
        }

        public async Task<int> getTemperatureByUserHometown(string homeTown)
        {
            int result = 0;
            var builder = new UriBuilder("https://api.openweathermap.org/data/2.5/weather");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["q"] = homeTown;
            query["units"] = "metric";
            query["appid"] = _webConfiguration.OpenWeatherAppId;
            builder.Query = query.ToString();
            string url = builder.ToString();
            
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            try
            {
                var response = await client.SendAsync(req);
                var responseMessage =(response.Content.ReadAsStringAsync().Result);
                JObject json = JObject.Parse(responseMessage);
                result = (int) json["main"]["temp"];
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return result;
        }
    }
}
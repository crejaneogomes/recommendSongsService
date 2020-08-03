using System.ComponentModel;
using System.Reflection;
using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using recommendSongsService.API.models.dto;
using recommendSongsService.Model;
using recommendSongsService.API.models;
using System.Web;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Options;

namespace recommendSongsService.API.Service
{
    public class RecommendSongsService : IRecommendSongsService
    {
        private readonly RecommendSongsDbContext _dbContext;
        private readonly WebConfiguration _webConfiguration;
        static HttpClient client;

        public RecommendSongsService(RecommendSongsDbContext dbContext, IOptionsMonitor<WebConfiguration> webConfiguration)
        {
            _dbContext = dbContext;
            _webConfiguration = webConfiguration.CurrentValue;
            client = new HttpClient();
        }

        public async Task<List<RecommendSongsDTO>> getRecommendedSongs(string userName)
        {
            var playlistId = ""; 
            var genre = "";
            List<RecommendSongsDTO> songs = new List<RecommendSongsDTO>();
            var user = _dbContext.Users.FirstOrDefault(x => x.Name == userName);
            var temperature = await getTemperatureByUserHometown(user.Hometown);
            var spotifyToken = await getSpotifyToken();

            if(temperature > 30)
            {
                playlistId = await getPlaylistsByGenre("party",spotifyToken);
                genre = "party";
            } else if (temperature >=15 && temperature <= 30)
            {
                playlistId = await getPlaylistsByGenre("pop",spotifyToken);
                genre = "pop";
            } else if (temperature >=10 && temperature <= 14)
            {
                playlistId = await getPlaylistsByGenre("rock",spotifyToken);
                genre = "rock";
            } else {
                playlistId = await getPlaylistsByGenre("classical",spotifyToken);
                genre = "classical";
            }
            songs = await getTraksOfSpotifyPlaylistById(playlistId, spotifyToken, genre);
            return songs;
        }

        private async Task<int> getTemperatureByUserHometown(string homeTown)
        {
            int result = 0;
            var builder = new UriBuilder("https://api.openweathermap.org/data/2.5/weather");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["q"] = homeTown;
            query["units"] = "metric";
            query["appid"] = WebConfiguration.OpenWeatherAppId;
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

        private async Task<string> getSpotifyToken()
        {
            var result = "";
            var req = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");
            req.Headers.Add("Authorization", "Basic " + WebConfiguration.SpotifyClientCredentials);
            req.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" }
            });

            try
            {
                var response = await client.SendAsync(req);
                var responseMessage =(response.Content.ReadAsStringAsync().Result);
                JObject json = JObject.Parse(responseMessage);
                result = (string) json["access_token"];
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return result;
        }

        private async Task<String> getPlaylistsByGenre(string genre, string token)
        {
            string result = "";            
            var req = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/browse/categories/{genre}/playlists");
            req.Headers.Add("Authorization", "Bearer " + token);
            try
            {
                var response = await client.SendAsync(req);
                var responseMessage =(response.Content.ReadAsStringAsync().Result);
                JObject json = JObject.Parse(responseMessage);
                result = (string) json["playlists"]["items"][0]["id"];
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return result;

        }


        private async Task<List<RecommendSongsDTO>> getTraksOfSpotifyPlaylistById(string playlistId, string token, string genre)
        {
            List<RecommendSongsDTO> result = new List<RecommendSongsDTO>();            
            var req = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/playlists/{playlistId}/tracks");
            req.Headers.Add("Authorization", "Bearer " + token);
            try
            {
                var response = await client.SendAsync(req);
                var responseMessage =(response.Content.ReadAsStringAsync().Result);
                JObject json = JObject.Parse(responseMessage);
                JArray a = (JArray)json["items"];
                foreach(var song in a)
                {
                    RecommendSongsDTO songToAdd = new RecommendSongsDTO()
                    {
                        Song = (string)song["track"]["name"],
                        Artist = (string)song["track"]["artists"][0]["name"],
                        Genre = genre,
                        Link = (string)song["track"]["href"]
                    };
                    result.Add(songToAdd);
                }
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return result;           
        }
    }
}
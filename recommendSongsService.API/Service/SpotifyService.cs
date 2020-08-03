using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using recommendSongsService.API.models.dto;

namespace recommendSongsService.API.Service
{
    public class SpotifyService
    {
        private readonly WebConfiguration _webConfiguration;
        private static HttpClient client;
        private static string spotifyToken;

        public SpotifyService(IOptionsMonitor<WebConfiguration> webConfiguration)
        {
            _webConfiguration = webConfiguration.CurrentValue;
            client = new HttpClient();
            spotifyToken = getSpotifyToken().Result;
        }

        public async Task<string> getSpotifyToken()
        {
            var result = "";
            var req = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");
            req.Headers.Add("Authorization", "Basic " + _webConfiguration.SpotifyClientCredentials);
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

        public async Task<String> getPlaylistsByGenre(string genre)
        {
            string result = "";            
            var req = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/browse/categories/{genre}/playlists");
            req.Headers.Add("Authorization", "Bearer " + spotifyToken);
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


        public async Task<List<RecommendSongsDTO>> getTraksOfSpotifyPlaylistById(string playlistId, string genre)
        {
            List<RecommendSongsDTO> result = new List<RecommendSongsDTO>();            
            var req = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/playlists/{playlistId}/tracks");
            req.Headers.Add("Authorization", "Bearer " + spotifyToken);
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
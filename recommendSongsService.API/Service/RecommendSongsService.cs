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
        private readonly SpotifyService _spotifyService;
        private readonly OpenWeatherMapService _openWeatherMapService;

        public RecommendSongsService(RecommendSongsDbContext dbContext, IOptionsMonitor<WebConfiguration> webConfiguration, SpotifyService spotifyService, OpenWeatherMapService openWeatherMapService)
        {
            _dbContext = dbContext;
            _webConfiguration = webConfiguration.CurrentValue;
            _spotifyService = spotifyService;
            _openWeatherMapService = openWeatherMapService;
        }

        public async Task<List<RecommendSongsDTO>> getRecommendedSongs(string userName)
        {
            var playlistId = ""; 
            var genre = "";

            List<RecommendSongsDTO> songs = new List<RecommendSongsDTO>();
            var user = _dbContext.Users.FirstOrDefault(x => x.Name == userName);
            var temperature = await _openWeatherMapService.getTemperatureByUserHometown(user.Hometown);

            if(temperature > 30)
            {
                playlistId = await _spotifyService.getPlaylistsByGenre("party");
                genre = "party";
            } else if (temperature >=15 && temperature <= 30)
            {
                playlistId = await _spotifyService.getPlaylistsByGenre("pop");
                genre = "pop";
            } else if (temperature >=10 && temperature <= 14)
            {
                playlistId = await _spotifyService.getPlaylistsByGenre("rock");
                genre = "rock";
            } else {
                playlistId = await _spotifyService.getPlaylistsByGenre("classical");
                genre = "classical";
            }
            songs = await _spotifyService.getTraksOfSpotifyPlaylistById(playlistId, genre);
            return songs;
        }
    }
}
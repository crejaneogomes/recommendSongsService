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
            playlistId = await _spotifyService.getPlaylistsByGenre(getGenreByTemperature(temperature));
            songs = await _spotifyService.getTraksOfSpotifyPlaylistById(playlistId, genre);
            return songs;
        }

        private string getGenreByTemperature(int temperature)
        {
            var genre = "";
            if(temperature > 30)
            {
                genre = "party";
            } else if (temperature >=15 && temperature <= 30)
            {
                genre = "pop";
            } else if (temperature >=10 && temperature <= 14)
            {
                genre = "rock";
            } else {
                genre = "classical";
            }
            return genre;
        }
    }
}
using Microsoft.Extensions.Configuration;

namespace recommendSongsService.API
{
    public class AppSettings
    {
        private IConfiguration _configuration;

        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string Secret;
        public static string OpenWeatherAppId;
        public static string SpotifyClientCredentials;
    }
}
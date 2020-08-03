using System.Collections.Generic;

namespace recommendSongsService.API.models.dto
{
    public class RecommendSongsDTO
    {
        public string Song { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public string Link { get; set; }
    }
}
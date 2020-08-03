using System.Collections.Generic;
using System.Threading.Tasks;
using recommendSongsService.API.models.dto;
using recommendSongsService.Model;

namespace recommendSongsService.API.Service
{
    public interface IRecommendSongsService
    {
        Task<List<RecommendSongsDTO>> getRecommendedSongs(string userName);
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using recommendSongsService.API.models.dto;
using recommendSongsService.API.Service;
using recommendSongsService.Model;

namespace recommendSongsService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecommendSongsController : ControllerBase
    {
        private readonly ILogger<RecommendSongsController> _logger;
        private readonly IRecommendSongsService RecommendSongsService;
        public RecommendSongsController(ILogger<RecommendSongsController> logger, IRecommendSongsService recommendSongsService)
        {
            this._logger = logger;
            this.RecommendSongsService = recommendSongsService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<dynamic>> Get()
        {
            var result = await RecommendSongsService.getRecommendedSongs(User.Identity.Name);
            if (result == null)
            {
                return NotFound(new { message = "Nao existe recomendacoes" });
            } else 
            {
                return result;
            }
        }
    }
}
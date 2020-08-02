using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using recommendSongsService.API.models.dto;
using recommendSongsService.API.Service;
using recommendSongsService.Model;

namespace recommendSongsService.API.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticateService AuthenticationService;
        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticateService authenticationService)
        {
            this._logger = logger;
            this.AuthenticationService = authenticationService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Post([FromBody]LoginDTO user)
        {
            var result = await AuthenticationService.Authenticate(user);
            if (result == null)
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            } else 
            {
                return result;
            }
        }
    }
}
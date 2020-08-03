using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using recommendSongsService.API.models.dto;
using recommendSongsService.API.Service;
using recommendSongsService.Model;

namespace recommendSongsService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService UserService;
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            this._logger = logger;
            this.UserService = userService;
        }

        [HttpPost]
        public async Task<User> Post(UserDTO user)
        {
            var result = await UserService.SaveUSer(user);
            _logger.LogInformation("Adding user " + result);
            return result;
        }
        
    }
}
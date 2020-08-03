using System.Collections.Generic;
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

        [HttpPut("ForgotPassword")]
        public Dictionary<string,string> ForgotUserPassword(string userEmail)
        {
            var result = UserService.ForgotUserPassword(userEmail);
            if(result == null)
            {
                _logger.LogInformation("User not found when Forgot User Password - " + userEmail);
                return new Dictionary<string, string>()
                {
                    {"Result", "Usuario nao encontrado"}
                };
            }
            _logger.LogInformation("Forgot Password for the user = " + userEmail);
            return result;
        }

        [HttpPut("ResetPassword")]
        public Dictionary<string,string> ResetUserPassword(ResetPasswordDTO user)
        {
            var result = UserService.ChangeUserPassword(user);
            if(result == null)
            {
                _logger.LogInformation("User not found when update the user password - " + user.Email);
                return new Dictionary<string, string>()
                {
                    {"Result", "Usuario nao encontrado"}
                };
            }
            _logger.LogInformation("Updata user password of user" + user.Email);
            return result;
        }
    }
}
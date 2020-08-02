using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using recommendSongsService.API.models.dto;
using recommendSongsService.Model;

namespace recommendSongsService.API.Service
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly RecommendSongsDbContext _dbContext;
        public AuthenticateService(RecommendSongsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Dictionary<string, string>> Authenticate(LoginDTO user)
        {
            return Task.Run(() => 
            {
                var userToAuthenticate = _dbContext.Users
                .FirstOrDefault(x => x.Email.Equals(user.Email) && x.Password.Equals(user.Password));

                // Verifica se o usuário existe
                if (userToAuthenticate == null)
                {
                    return null;
                } else
                {
                    // Gera o Token
                    var token = TokenService.GenerateToken(userToAuthenticate);

                    // Oculta a senha
                    userToAuthenticate.Password = "";

                    // Retorna os dados
                    var result = new Dictionary<string, string>()
                    {
                        { "user", userToAuthenticate.Name },
                        { "user_email", userToAuthenticate.Email },
                        { "token", token }
                    };
                    return result;
                }
            });           
        }
    }
}
using System.Threading.Tasks;
using recommendSongsService.API.models.dto;
using recommendSongsService.Model;

namespace recommendSongsService.API.Service
{
    public interface IUserService
    {
        Task<User> SaveUSer(UserDTO user);
    }
}
using TodoApi.Core.DTO;
using TodoApi.Core.Model;
namespace TodoApi.Core.Inerface
{
    public interface IAuthService
    {
        Task<User> Register(UserRegisterDto dto);
        Task<string> Login(UserLoginDto dto);
    }
}

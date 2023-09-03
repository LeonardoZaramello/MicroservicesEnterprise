using SE.WebApp.MVC.Models;

namespace SE.WebApp.MVC.Services
{
    public interface IAuthService
    {
        Task<UserResponseLogin> Login(UserLogin userLogin);

        Task<UserResponseLogin> Register(UserRegister userRegister);
    }
}

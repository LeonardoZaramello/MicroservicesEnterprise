using SE.WebApp.MVC.Models;
using System.Text;
using System.Text.Json;

namespace SE.WebApp.MVC.Services
{
    public class AuthService : Service, IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserResponseLogin> Login(UserLogin userLogin)
        {
            var loginContext = GetContent(userLogin);

            var response = await _httpClient.PostAsync("http://localhost:5035/api/identidade/autenticar", loginContext);

            if(!HandleErrorsResponse(response)) 
            {
                return new UserResponseLogin
                {
                    ResponseResult = await DeserializarResponseObject<ResponseResult>(response)
                };
            }

            return await DeserializarResponseObject<UserResponseLogin>(response);
        }

        public async Task<UserResponseLogin> Register(UserRegister userRegister)
        {
            var registerContent = GetContent(userRegister);

            var response = await _httpClient.PostAsync("http://localhost:5035/api/identidade/nova-conta", registerContent);

            if (!HandleErrorsResponse(response))
            {
                return new UserResponseLogin
                {
                    ResponseResult = await DeserializarResponseObject<ResponseResult>(response)
                };
            }

            return await DeserializarResponseObject<UserResponseLogin>(response);
        }
    }
}

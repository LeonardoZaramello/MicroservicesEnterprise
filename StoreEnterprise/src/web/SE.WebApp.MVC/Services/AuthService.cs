using Microsoft.Extensions.Options;
using SE.WebApp.MVC.Extensions;
using SE.WebApp.MVC.Models;

namespace SE.WebApp.MVC.Services
{
    public class AuthService : Service, IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AuthUrl);
            _httpClient = httpClient;
        }

        public async Task<UserResponseLogin> Login(UserLogin userLogin)
        {
            var loginContext = GetContent(userLogin);

            var response = await _httpClient.PostAsync("/api/identidade/autenticar", loginContext);

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

            var response = await _httpClient.PostAsync("/api/identidade/nova-conta", registerContent);

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

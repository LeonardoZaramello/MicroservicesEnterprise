using SE.WebApp.MVC.Models;
using System.Text;
using System.Text.Json;

namespace SE.WebApp.MVC.Services
{
    public class AuthService : IAuthService
    {
        //private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public AuthService(/*IHttpClientFactory httpClientFactory*/ HttpClient httpClient)
        {
            //_httpClientFactory = httpClientFactory;
            _httpClient = httpClient;
        }

        public async Task<UserResponseLogin> Login(UserLogin userLogin)
        {
            var loginContext = new StringContent(JsonSerializer.Serialize(userLogin), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5035/api/identidade/autenticar", loginContext);

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<UserResponseLogin>(await response.Content.ReadAsStringAsync(), serializeOptions)!;
        }

        public async Task<UserResponseLogin> Register(UserRegister userRegister)
        {
            var registerContent = new StringContent(JsonSerializer.Serialize(userRegister), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5035/api/identidade/nova-conta", registerContent);

            return JsonSerializer.Deserialize<UserResponseLogin>(await response.Content.ReadAsStringAsync())!;
        }
    }
}

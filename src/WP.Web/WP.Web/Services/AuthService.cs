using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using WP.Web.Interfaces;
using WP.Web.Models;

namespace WP.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUri;
        private const string RegisterEndpoint = "register";
        private const string LoginEndpoint = "login";

        public AuthService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _baseUri = _configuration.GetSection("IdentityApi:Url").Value!;
            _httpClientFactory = httpClientFactory;
        }

        Task<ActionResult> IAuthService.Login(UserLogin login)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult> Register(UserRegister userRegister)
        {
            var httpClient = _httpClientFactory.CreateClient(_baseUri);

            var userJson = new StringContent(JsonSerializer.Serialize(userRegister), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{_baseUri}{RegisterEndpoint}", userJson);

            if (response.IsSuccessStatusCode) return new OkResult();

            return new BadRequestResult();
        }
    }
}

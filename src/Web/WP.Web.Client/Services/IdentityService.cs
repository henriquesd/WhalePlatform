using System.Net.Http.Json;
using WP.Web.Client.Models;

namespace WP.Web.Client.Services
{
    public interface IIdentityService
    {
        Task<(bool Success, string Message, LoginResponseDto? Data)> RegisterAsync(RegisterUserDto registerDto);
        Task<(bool Success, string Message, LoginResponseDto? Data)> LoginAsync(LoginUserDto loginDto);
        Task LogoutAsync();
        Task<bool> IsAuthenticatedAsync();
    }

    public class IdentityService : BaseHttpService, IIdentityService
    {
        private const string TokenKey = "authToken";
        private const string UserEmailKey = "userEmail";

        public IdentityService(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<(bool Success, string Message, LoginResponseDto? Data)> RegisterAsync(RegisterUserDto registerDto)
        {
            try
            {
                var response = await PostAsync("api/identity/register", registerDto);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>(_jsonOptions);
                    
                    if (result != null)
                    {
                        await StoreAuthDataAsync(result);
                        return (true, "Registration successful!", result);
                    }
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                return (false, $"Registration failed: {errorMessage}", null);
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}", null);
            }
        }

        public async Task<(bool Success, string Message, LoginResponseDto? Data)> LoginAsync(LoginUserDto loginDto)
        {
            try
            {
                var response = await PostAsync("api/identity/login", loginDto);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>(_jsonOptions);
                    
                    if (result != null)
                    {
                        await StoreAuthDataAsync(result);
                        return (true, "Login successful!", result);
                    }
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                return (false, $"Login failed: {errorMessage}", null);
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}", null);
            }
        }

        public async Task LogoutAsync()
        {
            await Task.Run(() =>
            {
                // Clear auth data from local storage
                // In a real app, you'd use IJSRuntime to interact with localStorage
                Console.WriteLine("User logged out");
            });
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            // In a real app, check if token exists and is valid
            return await Task.FromResult(false);
        }

        private async Task StoreAuthDataAsync(LoginResponseDto loginResponse)
        {
            await Task.Run(() =>
            {
                // In a real app, store token in localStorage using IJSRuntime
                Console.WriteLine($"Token stored: {loginResponse.AccessToken}");
            });
        }
    }
}

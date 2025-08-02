using System.Net.Http.Json;
using BankingAppClient.Models;

namespace BankingAppClient.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    public string Token { get; private set; } = string.Empty;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> Register(AuthenticationModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/register", model);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> Login(AuthenticationModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", model);
        if (response.IsSuccessStatusCode)
        {
            var tokenModel = await response.Content.ReadFromJsonAsync<TokenModel>();
            if (tokenModel != null)
            {
                Token = tokenModel.Token;
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
                return true;
            }
        }
        return false;
    }
}

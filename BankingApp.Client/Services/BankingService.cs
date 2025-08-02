using System.Net.Http.Json;
using BankingApp.Client.Models;

namespace BankingApp.Client.Services;

public class BankingService
{
    private readonly HttpClient _httpClient;

    public BankingService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<AccountModel>?> GetAccounts()
    {
        return await _httpClient.GetFromJsonAsync<List<AccountModel>>("api/accounts");
    }

    public async Task<TransactionResponse?> Deposit(TransactionRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/accounts/" + request.AccountId + "/deposit", request);
        return await response.Content.ReadFromJsonAsync<TransactionResponse>();
    }

    public async Task<TransactionResponse?> Withdraw(TransactionRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/accounts/" + request.AccountId + "/withdraw", request);
        return await response.Content.ReadFromJsonAsync<TransactionResponse>();
    }

    public async Task<bool> CreateAccount(NewAccountRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/accounts", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<AccountModel?> GetAccountById(int accountId)
    {
        return await _httpClient.GetFromJsonAsync<AccountModel>($"api/accounts/{accountId}");
    }
}

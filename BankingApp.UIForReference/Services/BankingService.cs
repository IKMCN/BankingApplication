using System.Net.Http.Json;
using BankingAppClient.Models;

namespace BankingAppClient.Services;

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
}

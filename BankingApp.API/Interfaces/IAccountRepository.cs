using BankingApp.API.Models;

namespace BankingApp.API.Interfaces;

public interface IAccountRepository
{
    Task<IEnumerable<AccountModel>> GetAccountsByUserIdAsync(int userId);
    Task<AccountModel?> GetAccountByIdAsync(int accountId);
    Task<int> CreateAccountAsync(AccountModel account);
    Task UpdateBalanceAsync(int accountId, decimal newBalance);
}
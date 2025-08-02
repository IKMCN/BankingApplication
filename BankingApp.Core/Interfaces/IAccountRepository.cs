
using BankingApp.Core.Models;

namespace BankingApp.Core.Interfaces;

public interface IAccountRepository
{
    Task<IEnumerable<AccountModel>> GetAllAccountsAsync();
    Task<AccountModel?> GetAccountByIdAsync(int id);
    Task<AccountModel?> GetAccountByCardNumberAsync(string cardNumber);
    Task<AccountModel> CreateAccountAsync(AccountModel account);
    Task<AccountModel> UpdateAccountAsync(AccountModel account);
    Task<bool> DeleteAccountAsync(int id);
}
using BankingApp.Core.Models;
using BankingApp.Core.Interfaces;

namespace BankingApp.Core.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly ISqlDataAccess _db;

    public AccountRepository(ISqlDataAccess db)
    {
        _db = db;
    }

    public async Task<IEnumerable<AccountModel>> GetAllAccountsAsync()
    {
        return await _db.LoadData<AccountModel, dynamic>(
            "SELECT * FROM accounts", new { });
    }

    public async Task<AccountModel?> GetAccountByIdAsync(int accountId)
    {
        var result = await _db.LoadData<AccountModel, dynamic>(
            "SELECT * FROM accounts WHERE accountid = @AccountId", new { AccountId = accountId });
        return result.FirstOrDefault();
    }

    public async Task<AccountModel?> GetAccountByNumberAsync(string accountNumber)
    {
        var result = await _db.LoadData<AccountModel, dynamic>(
            "SELECT * FROM accounts WHERE accountnumber = @AccountNumber", new { AccountNumber = accountNumber });
        return result.FirstOrDefault();
    }

    public async Task<AccountModel> CreateAccountAsync(AccountModel account)
    {
        var result = await _db.LoadData<int, dynamic>(
            "INSERT INTO accounts (userid, accountnumber, accounttype, balance, createdat) VALUES (@UserId, @AccountNumber, @AccountType, @Balance, @CreatedAt) RETURNING accountid",
            new
            {
                account.UserId,
                account.AccountNumber,
                account.AccountType,
                account.Balance,
                account.CreatedAt
            });

        account.AccountId = result.FirstOrDefault();
        return account;
    }

    public async Task<AccountModel> UpdateAccountAsync(AccountModel account)
    {
        await _db.SaveData(
            "UPDATE accounts SET accounttype = @AccountType, balance = @Balance WHERE accountid = @AccountId",
            new
            {
                account.AccountId,
                account.AccountType,
                account.Balance
            });
        return account;
    }

    public async Task<bool> DeleteAccountAsync(int accountId)
    {
        await _db.SaveData(
            "DELETE FROM accounts WHERE accountid = @AccountId",
            new { AccountId = accountId });
        return true;
    }

    public Task<AccountModel?> GetAccountByCardNumberAsync(string cardNumber)
    {
        throw new NotImplementedException();
    }
}

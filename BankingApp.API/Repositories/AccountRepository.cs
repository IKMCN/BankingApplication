using Dapper;
using Npgsql;
using BankingApp.API.Interfaces;
using BankingApp.API.Models;

namespace BankingApp.API.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly string _connectionString;

    public AccountRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<AccountModel>> GetAccountsByUserIdAsync(int userId)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "SELECT * FROM Accounts WHERE UserId = @userId";
        return await connection.QueryAsync<AccountModel>(sql, new { userId });
    }

    public async Task<AccountModel?> GetAccountByIdAsync(int accountId)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "SELECT * FROM Accounts WHERE AccountId = @accountId";
        return await connection.QueryFirstOrDefaultAsync<AccountModel>(sql, new { accountId });
    }

    public async Task<int> CreateAccountAsync(AccountModel account)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"INSERT INTO Accounts (UserId, AccountNumber, AccountType, Balance, CreatedAt) 
                       VALUES (@UserId, @AccountNumber, @AccountType, @Balance, @CreatedAt) 
                       RETURNING AccountId";
        return await connection.QuerySingleAsync<int>(sql, account);
    }

    public async Task UpdateBalanceAsync(int accountId, decimal newBalance)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "UPDATE Accounts SET Balance = @newBalance WHERE AccountId = @accountId";
        await connection.ExecuteAsync(sql, new { newBalance, accountId });
    }
}
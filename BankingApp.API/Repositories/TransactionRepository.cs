using Dapper;
using Npgsql;
using BankingApp.API.Interfaces;
using BankingApp.API.Models;

namespace BankingApp.API.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly string _connectionString;

    public TransactionRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<int> CreateTransactionAsync(TransactionModel transaction)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"INSERT INTO Transactions (AccountId, Amount, TransactionType, Description, CreatedAt) 
                       VALUES (@AccountId, @Amount, @TransactionType, @Description, @CreatedAt) 
                       RETURNING TransactionId";
        return await connection.QuerySingleAsync<int>(sql, transaction);
    }

    public async Task<IEnumerable<TransactionModel>> GetTransactionsByAccountIdAsync(int accountId)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "SELECT * FROM Transactions WHERE AccountId = @accountId ORDER BY CreatedAt DESC";
        return await connection.QueryAsync<TransactionModel>(sql, new { accountId });
    }
}
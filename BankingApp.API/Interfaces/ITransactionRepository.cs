using BankingApp.API.Models;

namespace BankingApp.API.Interfaces;

public interface ITransactionRepository
{
    Task<int> CreateTransactionAsync(TransactionModel transaction);
    Task<IEnumerable<TransactionModel>> GetTransactionsByAccountIdAsync(int accountId);
}
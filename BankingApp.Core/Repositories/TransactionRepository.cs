using BankingApp.Core.Models;
using BankingApp.Core.Interfaces;

namespace BankingApp.Core.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly ISqlDataAccess _db;

    public TransactionRepository(ISqlDataAccess db)
    {
        _db = db;
    }

    public async Task<IEnumerable<TransactionModel>> GetAllTransactionsAsync()
    {
        return await _db.LoadData<TransactionModel, dynamic>(
            "SELECT * FROM transactions", new { });
    }

    public async Task<TransactionModel?> GetTransactionByIdAsync(int transactionId)
    {
        var result = await _db.LoadData<TransactionModel, dynamic>(
            "SELECT * FROM transactions WHERE transactionid = @TransactionId",
            new { TransactionId = transactionId });
        return result.FirstOrDefault();
    }

    public async Task<IEnumerable<TransactionModel>> GetTransactionsByAccountIdAsync(int accountId)
    {
        return await _db.LoadData<TransactionModel, dynamic>(
            "SELECT * FROM transactions WHERE accountid = @AccountId ORDER BY createdat DESC",
            new { AccountId = accountId });
    }

    public async Task<TransactionModel> CreateTransactionAsync(TransactionModel transaction)
    {
        if (string.IsNullOrEmpty(transaction.ReferenceNumber))
        {
            transaction.ReferenceNumber = TransactionModel.GenerateReferenceNumber();
        }

        var result = await _db.LoadData<int, dynamic>(
            "INSERT INTO transactions (accountid, amount, transactiontype, description, createdat, status, referencenumber, balanceafter) VALUES (@AccountId, @Amount, @TransactionType, @Description, @CreatedAt, @Status, @ReferenceNumber, @BalanceAfter) RETURNING transactionid",
            new
            {
                transaction.AccountId,
                transaction.Amount,
                transaction.TransactionType,
                transaction.Description,
                transaction.CreatedAt,
                transaction.Status,
                transaction.ReferenceNumber,
                transaction.BalanceAfter
            });

        transaction.TransactionId = result.FirstOrDefault();
        return transaction;
    }

    public async Task<TransactionModel> UpdateTransactionAsync(TransactionModel transaction)
    {
        await _db.SaveData(
            "UPDATE transactions SET amount = @Amount, transactiontype = @TransactionType, description = @Description, status = @Status, balanceafter = @BalanceAfter, referencenumber = @ReferenceNumber WHERE transactionid = @TransactionId",
            new
            {
                transaction.TransactionId,
                transaction.Amount,
                transaction.TransactionType,
                transaction.Description,
                transaction.Status,
                transaction.BalanceAfter,
                transaction.ReferenceNumber
            });

        return transaction;
    }

    public async Task<bool> DeleteTransactionAsync(int transactionId)
    {
        await _db.SaveData("DELETE FROM transactions WHERE transactionid = @TransactionId",
            new { TransactionId = transactionId });
        return true;
    }

    public async Task<TransactionModel?> GetTransactionByReferenceNumberAsync(string referenceNumber)
    {
        var result = await _db.LoadData<TransactionModel, dynamic>(
            "SELECT * FROM transactions WHERE referencenumber = @ReferenceNumber",
            new { ReferenceNumber = referenceNumber });
        return result.FirstOrDefault();
    }

    public async Task<IEnumerable<TransactionModel>> GetRecentTransactionsAsync(int accountId, int count = 10)
    {
        return await _db.LoadData<TransactionModel, dynamic>(
            "SELECT * FROM transactions WHERE accountid = @AccountId ORDER BY createdat DESC LIMIT @Count",
            new { AccountId = accountId, Count = count });
    }

    public async Task<decimal> GetTotalTransactionAmountAsync(int accountId, string transactionType)
    {
        var result = await _db.LoadData<decimal, dynamic>(
            "SELECT COALESCE(SUM(amount), 0) FROM transactions WHERE accountid = @AccountId AND transactiontype = @TransactionType AND status = 'Completed'",
            new { AccountId = accountId, TransactionType = transactionType });

        return result.FirstOrDefault();
    }
}

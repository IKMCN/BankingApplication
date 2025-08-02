using BankingApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Core.Interfaces;



    public interface ITransactionRepository
    {
        Task<IEnumerable<TransactionModel>> GetAllTransactionsAsync();
        Task<TransactionModel?> GetTransactionByIdAsync(int id);
        Task<IEnumerable<TransactionModel>> GetTransactionsByAccountIdAsync(int accountId);
        Task<TransactionModel> CreateTransactionAsync(TransactionModel transaction);
        Task<TransactionModel> UpdateTransactionAsync(TransactionModel transaction);
        Task<bool> DeleteTransactionAsync(int id);
        Task<TransactionModel?> GetTransactionByReferenceNumberAsync(string referenceNumber);
        Task<IEnumerable<TransactionModel>> GetRecentTransactionsAsync(int accountId, int count = 10);
        Task<decimal> GetTotalTransactionAmountAsync(int accountId, string transactionType);
    }


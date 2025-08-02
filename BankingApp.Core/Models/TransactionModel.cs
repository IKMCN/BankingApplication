namespace BankingApp.Core.Models;

public class TransactionModel
{
    public int TransactionId { get; set; }
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public string TransactionType { get; set; } = "Deposit"; // "Deposit", "Withdrawal", "Transfer"
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Completed"; // "Pending", "Completed", "Failed"
    public string ReferenceNumber { get; set; } = string.Empty;
    public decimal BalanceAfter { get; set; }

    public bool IsValid() => Amount > 0 && AccountId > 0;

    public static string GenerateReferenceNumber()
    {
        return $"TXN{DateTime.UtcNow:yyyyMMddHHmmss}{Random.Shared.Next(1000, 9999)}";
    }
}

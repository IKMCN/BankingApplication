namespace BankingApp.Core.Models;

public class AccountModel
{
    public int AccountId { get; set; }
    public int UserId { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public string AccountType { get; set; } = "Checking";
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Optional logic (if used in Core services)
    public bool CanWithdraw(decimal amount) => Balance >= amount && amount > 0;
    public bool CanDeposit(decimal amount) => amount > 0;

    public void Withdraw(decimal amount)
    {
        if (!CanWithdraw(amount))
            throw new InvalidOperationException("Insufficient funds");
        Balance -= amount;
    }

    public void Deposit(decimal amount)
    {
        if (!CanDeposit(amount))
            throw new InvalidOperationException("Invalid deposit amount");
        Balance += amount;
    }
}

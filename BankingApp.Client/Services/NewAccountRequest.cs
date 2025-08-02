namespace BankingApp.Client.Models;

public class NewAccountRequest
{
    public string AccountType { get; set; } = string.Empty;
    public decimal InitialBalance { get; set; }
}
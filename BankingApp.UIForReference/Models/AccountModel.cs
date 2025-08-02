namespace BankingAppClient.Models;

public class AccountModel
{
    public int Id { get; set; }
    public string AccountHolder { get; set; } = string.Empty;
    public decimal Balance { get; set; }
}

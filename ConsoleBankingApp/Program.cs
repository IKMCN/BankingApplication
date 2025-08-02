using BankingApp.Core.Interfaces;     // ← Add this for interfaces
using BankingApp.Core.Repositories;
using Microsoft.Extensions.Configuration;

// Create configuration
var config = new ConfigurationBuilder()
    .AddInMemoryCollection(new Dictionary<string, string>
    {
        {"ConnectionStrings:Default", "Host=localhost;Database=banking_app;Username=postgres;Password=test123;Port=5432"}
    })
    .Build();

// Test connection
try
{
    var sqlDataAccess = new SqlDataAccess(config);
    var accountRepo = new AccountRepository(sqlDataAccess);  // ← FIXED: AccountRepository not AccountData

    Console.WriteLine("🔄 Testing database connection...");

    var accounts = await accountRepo.GetAllAccountsAsync();  // ← FIXED: GetAllAccountsAsync not GetAccountsAsync
    Console.WriteLine($"✅ Success! Found {accounts.Count()} accounts:");

    foreach (var account in accounts)
    {
        Console.WriteLine($"   - {account.Name}: {account.CardNumber} (${account.Balance})");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Connection failed: {ex.Message}");
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();
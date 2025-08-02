using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BankingApp.API.Interfaces;
using BankingApp.API.Models;

namespace BankingApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // 🔒 All endpoints require JWT authentication
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public AccountsController(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserAccounts()
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == 0) return Unauthorized();

                var accounts = await _accountRepository.GetAccountsByUserIdAsync(userId);
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving accounts", error = ex.Message });
            }
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetAccount(int accountId)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == 0) return Unauthorized();

                var account = await _accountRepository.GetAccountByIdAsync(accountId);

                if (account == null)
                    return NotFound(new { message = "Account not found" });

                if (account.UserId != userId)
                    return Forbid("You don't have access to this account");

                return Ok(account);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving account", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(CreateAccountRequest request)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == 0) return Unauthorized();

                var newAccount = new AccountModel
                {
                    UserId = userId,
                    AccountNumber = GenerateAccountNumber(),
                    AccountType = request.AccountType,
                    Balance = request.InitialBalance,
                    CreatedAt = DateTime.UtcNow
                };

                var accountId = await _accountRepository.CreateAccountAsync(newAccount);
                newAccount.AccountId = accountId;

                return Created("", newAccount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating account", error = ex.Message });
            }
        }

        [HttpPost("{accountId}/deposit")]
        public async Task<IActionResult> Deposit(int accountId, TransactionRequest request)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == 0) return Unauthorized();

                var account = await _accountRepository.GetAccountByIdAsync(accountId);
                if (account == null || account.UserId != userId)
                    return BadRequest(new { message = "Invalid account" });

                if (request.Amount <= 0)
                    return BadRequest(new { message = "Amount must be greater than zero" });

                var transaction = new TransactionModel
                {
                    AccountId = accountId,
                    Amount = request.Amount,
                    TransactionType = "Deposit",
                    Description = request.Description ?? "ATM Deposit",
                    CreatedAt = DateTime.UtcNow
                };

                var transactionId = await _transactionRepository.CreateTransactionAsync(transaction);
                await _accountRepository.UpdateBalanceAsync(accountId, account.Balance + request.Amount);

                transaction.TransactionId = transactionId;

                return Ok(new { message = "Deposit successful", transaction });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error processing deposit", error = ex.Message });
            }
        }

        [HttpPost("{accountId}/withdraw")]
        public async Task<IActionResult> Withdraw(int accountId, TransactionRequest request)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == 0) return Unauthorized();

                var account = await _accountRepository.GetAccountByIdAsync(accountId);
                if (account == null || account.UserId != userId)
                    return BadRequest(new { message = "Invalid account" });

                if (request.Amount <= 0)
                    return BadRequest(new { message = "Amount must be greater than zero" });

                if (account.Balance < request.Amount)
                    return BadRequest(new { message = "Insufficient funds" });

                var transaction = new TransactionModel
                {
                    AccountId = accountId,
                    Amount = -request.Amount,
                    TransactionType = "Withdrawal",
                    Description = request.Description ?? "ATM Withdrawal",
                    CreatedAt = DateTime.UtcNow
                };

                var transactionId = await _transactionRepository.CreateTransactionAsync(transaction);
                await _accountRepository.UpdateBalanceAsync(accountId, account.Balance - request.Amount);

                transaction.TransactionId = transactionId;

                return Ok(new { message = "Withdrawal successful", transaction });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error processing withdrawal", error = ex.Message });
            }
        }

        [HttpGet("{accountId}/transactions")]
        public async Task<IActionResult> GetTransactionHistory(int accountId)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == 0) return Unauthorized();

                var account = await _accountRepository.GetAccountByIdAsync(accountId);
                if (account == null || account.UserId != userId)
                    return BadRequest(new { message = "Invalid account" });

                var transactions = await _transactionRepository.GetTransactionsByAccountIdAsync(accountId);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving transactions", error = ex.Message });
            }
        }

        // Helper methods
        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out int userId) ? userId : 0;
        }

        private string GenerateAccountNumber()
        {
            var random = new Random();
            var accountNumber = "";
            for (int i = 0; i < 16; i++)
            {
                accountNumber += random.Next(0, 10);
            }
            return accountNumber;
        }
    }

    public class CreateAccountRequest
    {
        public string AccountType { get; set; } = "Checking";
        public decimal InitialBalance { get; set; } = 0;
    }

    public class TransactionRequest
    {
        public decimal Amount { get; set; }
        public string? Description { get; set; }
    }
}
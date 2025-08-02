using BankingApp.Core.Models;

namespace BankingApp.Core.Interfaces;

public interface IUserRepository
{
    Task<UserModel?> ValidateUserAsync(string username, string password);
    Task<UserModel?> GetUserByIdAsync(int userId);
    Task<UserModel?> GetUserByUsernameAsync(string username);
    Task<UserModel?> GetUserByEmailAsync(string email);
    Task<int> CreateUserAsync(UserModel user);
    Task<IEnumerable<UserModel>> GetAllUsersAsync();
}
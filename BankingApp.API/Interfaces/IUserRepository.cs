using BankingApp.API.Models;

namespace BankingApp.API.Interfaces;

public interface IUserRepository
{
    Task<UserModel?> ValidateUserAsync(string username, string password);
    Task<UserModel?> GetUserByUsernameAsync(string username);
    Task<UserModel?> GetUserByEmailAsync(string email);
    Task<int> CreateUserAsync(UserModel user);
    Task<UserModel?> GetUserByIdAsync(int userId);
}

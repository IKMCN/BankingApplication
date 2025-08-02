using BankingApp.Core.Models;
using BankingApp.Core.Interfaces;

namespace BankingApp.Core.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ISqlDataAccess _db;

    public UserRepository(ISqlDataAccess db)
    {
        _db = db;
    }

    public async Task<UserModel?> ValidateUserAsync(string username, string password)
    {
        var user = await _db.LoadData<UserModel, dynamic>(
            "SELECT * FROM users WHERE username = @Username",
            new { Username = username });

        if (user.FirstOrDefault() is not UserModel found)
            return null;

        // Assumes password is already hashed in DB and you are checking using BCrypt elsewhere
        return found;
    }

    public async Task<UserModel?> GetUserByIdAsync(int userId)
    {
        var result = await _db.LoadData<UserModel, dynamic>(
            "SELECT * FROM users WHERE userid = @UserId",
            new { UserId = userId });
        return result.FirstOrDefault();
    }

    public async Task<UserModel?> GetUserByUsernameAsync(string username)
    {
        var result = await _db.LoadData<UserModel, dynamic>(
            "SELECT * FROM users WHERE username = @Username",
            new { Username = username });
        return result.FirstOrDefault();
    }

    public async Task<UserModel?> GetUserByEmailAsync(string email)
    {
        var result = await _db.LoadData<UserModel, dynamic>(
            "SELECT * FROM users WHERE email = @Email",
            new { Email = email });
        return result.FirstOrDefault();
    }

    public async Task<int> CreateUserAsync(UserModel user)
    {
        var result = await _db.LoadData<int, dynamic>(
            "INSERT INTO users (username, email, password, createdat, isactive) VALUES (@Username, @Email, @Password, @CreatedAt, @IsActive) RETURNING userid",
            new
            {
                user.Username,
                user.Email,
                user.Password,
                user.CreatedAt,
                user.IsActive
            });

        return result.FirstOrDefault();
    }

    public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
    {
        return await _db.LoadData<UserModel, dynamic>("SELECT * FROM users", new { });
    }
}

using Dapper;
using Npgsql;
using BankingApp.API.Interfaces;
using BankingApp.API.Models;

namespace BankingApp.API.Repositories;
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<UserModel?> ValidateUserAsync(string username, string password)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "SELECT * FROM users WHERE username = @username";
            var user = await connection.QueryFirstOrDefaultAsync<UserModel>(sql, new { username });

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return user;
            }
            return null;
        }

        public async Task<UserModel?> GetUserByUsernameAsync(string username)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "SELECT * FROM users WHERE username = @username";
            return await connection.QueryFirstOrDefaultAsync<UserModel>(sql, new { username });
        }

        public async Task<UserModel?> GetUserByEmailAsync(string email)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "SELECT * FROM users WHERE email = @email";
            return await connection.QueryFirstOrDefaultAsync<UserModel>(sql, new { email });
        }

        public async Task<int> CreateUserAsync(UserModel user)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = @"INSERT INTO users (username, email, password, createdat) 
                       VALUES (@username, @email, @password, @createdat) 
                       RETURNING userid";
            return await connection.QuerySingleAsync<int>(sql, user);
        }

        public async Task<UserModel?> GetUserByIdAsync(int userId)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "SELECT * FROM users WHERE userid = @userid";
            return await connection.QueryFirstOrDefaultAsync<UserModel>(sql, new { userId });
        }
    }

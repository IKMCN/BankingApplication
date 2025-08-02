using BankingApp.Core.Interfaces;
using Dapper;
using Npgsql;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BankingApp.Core.Repositories
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)  // ← Takes IConfiguration
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(
            string sql,
            U parameters,
            string connectionId = "Default")
        {
            var connectionString = _config.GetConnectionString(connectionId);
            using IDbConnection connection = new NpgsqlConnection(connectionString);
            return await connection.QueryAsync<T>(sql, parameters);
        }

        public async Task SaveData<T>(
            string sql,
            T parameters,
            string connectionId = "Default")
        {
            using IDbConnection connection = new NpgsqlConnection(_config.GetConnectionString(connectionId));
            await connection.ExecuteAsync(sql, parameters);
        }
    }
}
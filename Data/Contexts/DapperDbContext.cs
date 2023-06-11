using Core.Configurations;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Data.Contexts
{
    public class DapperDbContext
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        private readonly string databaseType;

        public DapperDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.databaseType = configuration.GetConnectionString(AppSettings.DbType);
            this.connectionString = GetConnection();
        }

        private string GetConnection()
        {
            switch (this.databaseType)
            {
                case "Mssql":
                    return configuration.GetConnectionString(AppSettings.MsSqlConnection);
                case "PostgreSql":
                    return configuration.GetConnectionString(AppSettings.PostgreSqlConnection);
                default:
                    return configuration.GetConnectionString(AppSettings.DefaultConnection);

            }
        }

        public IDbConnection CreateConnection()
        {
            switch (this.databaseType)
            {
                case "Mssql":
                    return new SqlConnection(connectionString);
                case "PostgreSql":
                    return new NpgsqlConnection(connectionString);
                default:
                    return new SqlConnection(connectionString);
            }
        }
    }
}
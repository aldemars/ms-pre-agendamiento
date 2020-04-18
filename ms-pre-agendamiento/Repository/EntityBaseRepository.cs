namespace Ms_pre_agendamiento.Repository
{
    using System;
    using System.Data.SqlClient;
    using Microsoft.Extensions.Configuration;

    public class EntityBaseRepository
    {
        protected string _connectionString { get; }

        public EntityBaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("database");
        }

        private protected T ExecuteCommand<T>(string connStr, Func<SqlConnection, T> task)
        {
            using var connection = new SqlConnection(connStr);
            connection.Open();
            return task(connection);
        }

        private void ExecuteCommand(string connStr, Action<SqlConnection> task)
        {
            using var connection = new SqlConnection(connStr);
            connection.Open();
            task(connection);
        }

    }
}
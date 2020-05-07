using System;
using System.Data.Common;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ms_pre_agendamiento.Repository
{
    public class RepositoryCommandExecuter : IRepositoryCommandExecuter
    {
        private readonly string _connStr;
        private readonly string connectionStringKey = "database";

        public RepositoryCommandExecuter(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString(connectionStringKey);
        }
        
        public void ExecuteCommand(Action<DbConnection> task)
        {
            using (var conn = new  NpgsqlConnection(_connStr))
            {
                conn.Open();

                task(conn);
            }
        }
        public T ExecuteCommand<T>(Func<DbConnection, T> task)
        {
            using (var conn = new NpgsqlConnection(_connStr))
            {
                conn.Open();

                return task(conn);
            }
        }
    }
}
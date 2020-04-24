using System;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

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
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();

                task(conn);
            }
        }
        public T ExecuteCommand<T>(Func<DbConnection, T> task)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();

                return task(conn);
            }
        }
    }
}
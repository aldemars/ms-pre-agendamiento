using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ms_pre_agendamiento.Repository
{
    public class EntityBaseRepository
    {
        private readonly string _connStr;
        
        public string ConnectionString { 
            get {return _connStr;} 
        }
        
        
        public EntityBaseRepository(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("database");
        }
        
        private void ExecuteCommand(string connStr, Action<SqlConnection> task)
        {
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();

                task(conn);
            }
        }
        private protected T ExecuteCommand<T>(string connStr, Func<SqlConnection, T> task)
        {
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();

                return task(conn);
            }
        }
    }
}
using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using ms_pre_agendamiento.Repository;

namespace ms_pre_agendamiento.Tests
{
    public class RepositoryMemoryCommandExecuter : IRepositoryCommandExecuter
    {
        private DbConnection cnx;
        private IConfiguration _configuration;
        
        public RepositoryMemoryCommandExecuter(IConfiguration configuration)
        {
            _configuration = configuration;
            string connStr = configuration.GetConnectionString("database");
            cnx = new SqliteConnection(connStr);
            CheckDatabaseMigrations();
        }
        public void ExecuteCommand(Action<DbConnection> task)
        {
            throw new NotImplementedException();
        }

        public T ExecuteCommand<T>(Func<DbConnection, T> task)
        {
            using (cnx)
            {
                cnx.Open();

                return task(cnx);
            }
        }
        
        private void CheckDatabaseMigrations()
        {
         
            var location = _configuration.GetSection("AppSettings")["Evolve.Location"];
            var evolve = new Evolve.Evolve(cnx)
            {
                Locations = new[] {location},
                IsEraseDisabled = true,
            };

            evolve.Migrate();
        }
        
    }
}
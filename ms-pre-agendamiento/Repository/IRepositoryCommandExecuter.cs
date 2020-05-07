using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace ms_pre_agendamiento.Repository
{
    public interface IRepositoryCommandExecuter
    {
        void ExecuteCommand(Action<DbConnection> task);
        T ExecuteCommand<T>(Func<DbConnection, T> task);
        void HealthCheck();
    }
}
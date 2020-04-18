using System;
using System.Data.SqlClient;

namespace ms_pre_agendamiento.Repository
{
    public interface IRepositoryCommandExecuter
    {
        void ExecuteCommand(Action<SqlConnection> task);
        T ExecuteCommand<T>(Func<SqlConnection, T> task);
    }
}
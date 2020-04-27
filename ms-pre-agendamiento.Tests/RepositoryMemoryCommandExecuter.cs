using System;
using System.Data.Common;
using System.Linq;
using Dapper;
using Moq;
using ms_pre_agendamiento.Models;
using ms_pre_agendamiento.Repository;

namespace ms_pre_agendamiento.Tests
{
    public class RepositoryMemoryCommandExecuter : IRepositoryCommandExecuter
    {
        private IRepositoryCommandExecuter SetupMock()
        {
            User genericResponse = new User() {Id = "1", Name = "genericUser", Password = "superSecure", Role = "scheduler"};
            User userId2 = new User() {Id = "2", Name = "Juan", Password = "superSecure", Role = "scheduler"};

            var command = new Mock<IRepositoryCommandExecuter>();
            command
                .Setup(c => c.ExecuteCommand<User>(It.IsAny<Func<DbConnection, User>>()))
                .Returns((Func<DbConnection, User> task) => { return genericResponse; });


            var function = new Func<DbConnection, User>(conn =>
                conn.Query<User>( UserCommand.GetByUserNameAndPassword, new {@Name = "doesntExistUserName", @Password = "password"})
                    .SingleOrDefault());
            command.Setup(c => c.ExecuteCommand<User>(function))
                .Returns((Func<DbConnection, User> task) => { return null; });


            return command.Object;
        }

        public void ExecuteCommand(Action<DbConnection> task)
        {
            throw new NotImplementedException();
        }

        public T ExecuteCommand<T>(Func<DbConnection, T> task)
        {
            return SetupMock().ExecuteCommand(task);
        }
    }
}
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Moq;
using ms_pre_agendamiento.Models;
using ms_pre_agendamiento.Repository;
using Xunit;

namespace ms_pre_agendamiento.Tests.Repository
{
    public class UserRepositoryTest
    {
        private IRepositoryCommandExecuter SetupMock()
        {
            User expected = new User() {Id = "1", Name = "myName", Password = "superSecure"};
            var command = new Mock<IRepositoryCommandExecuter>();
            command
                .Setup(c => c.ExecuteCommand<User>(It.IsAny<Func<DbConnection, User>>()))
                .Returns((Func<SqlConnection, User> task) => { return expected; });

            return command.Object;
        }

        [Fact]
        public void ShouldReturnValidUserWhenIsRequestedByUserNameAndPassword()
        {
            string expectedName = "myName";
            string expectedPassword = "superSecure";
            // Arrange
            var command = SetupMock();

            IUserRepository userRepository =
                new UserRepository(command);

            // Act
            User userResult = userRepository.GetByUserNameAndPassword(expectedName, expectedPassword);

            // Assert
            Assert.Equal(expectedName, userResult.Name);
            Assert.Equal(expectedPassword, userResult.Password);
        }

        [Fact]
        public void ShouldReturnValidUserWhenIsRequestedById()
        {
            string expectedID = "1";
            string expectedName = "myName";
            string expectedPassword = "superSecure";
            // Arrange
            var command = SetupMock();

            IUserRepository userRepository =
                new UserRepository(command);

            // Act
            User userResult = userRepository.GetById(1);

            // Assert
            Assert.Equal(expectedID, userResult.Id);
            Assert.Equal(expectedName, userResult.Name);
            Assert.Equal(expectedPassword, userResult.Password);
        }
    }
}
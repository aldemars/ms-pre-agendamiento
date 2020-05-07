using System;
using System.Data.Common;
using Moq;
using ms_pre_agendamiento.Models;
using ms_pre_agendamiento.Repository;
using Npgsql;
using Xunit;

namespace ms_pre_agendamiento.Tests.Repository
{
    public class UserRepositoryTest
    {
        private IRepositoryCommandExecuter SetupMock()
        {
            var expected = new User() {Id = "1", Name = "myName", Password = "superSecure"};
            var command = new Mock<IRepositoryCommandExecuter>();
            command
                .Setup(c => c.ExecuteCommand(It.IsAny<Func<DbConnection, User>>()))
                .Returns((Func<NpgsqlConnection, User> task) => expected);

            return command.Object;
        }

        [Fact]
        public void ShouldReturnValidUserWhenIsRequestedByUserNameAndPassword()
        {
            const string expectedName = "myName";
            const string expectedPassword = "superSecure";
            // Arrange
            var command = SetupMock();

            IUserRepository userRepository =
                new UserRepository(command);

            // Act
            var userResult = userRepository.GetByUserNameAndPassword(expectedName, expectedPassword);

            // Assert
            Assert.Equal(expectedName, userResult.Name);
            Assert.Equal(expectedPassword, userResult.Password);
        }

        [Fact]
        public void ShouldReturnValidUserWhenIsRequestedById()
        {
            const string expectedID = "1";
            const string expectedName = "myName";
            const string expectedPassword = "superSecure";
            // Arrange
            var command = SetupMock();

            IUserRepository userRepository =
                new UserRepository(command);

            // Act
            var userResult = userRepository.GetById(1);

            // Assert
            Assert.Equal(expectedID, userResult.Id);
            Assert.Equal(expectedName, userResult.Name);
            Assert.Equal(expectedPassword, userResult.Password);
        }
    }
}
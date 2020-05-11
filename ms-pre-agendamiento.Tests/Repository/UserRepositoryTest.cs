using System;
using System.Collections.Generic;
using System.Data.Common;
using Moq;
using ms_pre_agendamiento.Dto;
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
            var user = new User() {Id = "1", Name = "myName", Password = "superSecure"};
            List<Appointment> appointments = new List<Appointment>();
            appointments.Add(new Appointment() {Id = 1, Hour = new TimeSpan(), Description = "description", HealthcareFacilityId = 1});
            user.Appointments = appointments;
            
            
            var command = new Mock<IRepositoryCommandExecuter>();
            command
                .Setup(c => c.ExecuteCommand(It.IsAny<Func<DbConnection, User>>()))
                .Returns((Func<NpgsqlConnection, User> task) => user);

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

        [Fact]
        public void ShouldReturnUserAppointmentsWhenIsIsValid()
        {
            const string expectedID = "1";
            const string expectedName = "myName";
            const string expectedPassword = "superSecure";
            const int expectedSize = 1;
            
            var command = SetupMock();  
            IUserRepository userRepository =
                new UserRepository(command);

            var userResult = userRepository.GetUserAppointmentsById(1);
            
            // Assert
            Assert.Equal(expectedID, userResult.Id);
            Assert.Equal(expectedName, userResult.Name);
            Assert.Equal(expectedPassword, userResult.Password);
            Assert.NotEmpty(userResult.Appointments);
            Assert.Equal(expectedSize,userResult.Appointments.Count);
            

        }
    }
}
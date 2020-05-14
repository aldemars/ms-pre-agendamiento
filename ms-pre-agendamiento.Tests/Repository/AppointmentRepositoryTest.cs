using System;
using System.Collections.Generic;
using System.Data.Common;
using Moq;
using ms_pre_agendamiento.Models;
using ms_pre_agendamiento.Repository;
using ms_pre_agendamiento.Repository.Impl;
using Npgsql;
using Xunit;

namespace ms_pre_agendamiento.Tests.Repository
{
    public class AppointmentRepositoryTest
    {
        private IRepositoryCommandExecuter SetupMock()
        {
            List<Appointment> appointments = new List<Appointment>
            {
                new Appointment()
                {
                    Id = 1,
                    Hour = new TimeSpan(),
                    Description = "description",
                    HealthcareFacilityId = 1,
                    Type = "userAppointment"
                },
                new Appointment()
                {
                    Id = 2,
                    Hour = new TimeSpan(),
                    Description = "description",
                    HealthcareFacilityId = 1,
                    Type = "userAppointment"
                },
                new Appointment()
                {
                    Id = 3,
                    Hour = new TimeSpan(),
                    Description = "description",
                    HealthcareFacilityId = 2,
                    Type = "centerAppointment"
                },
                new Appointment()
                {
                    Id = 4,
                    Hour = new TimeSpan(),
                    Description = "description",
                    HealthcareFacilityId = 2,
                    Type = "centerAppointment"
                }
            };

            var command = new Mock<IRepositoryCommandExecuter>();
            command
                .Setup(c => c.ExecuteCommand(It.IsAny<Func<DbConnection, List<Appointment>>>()))
                .Returns((Func<NpgsqlConnection, List<Appointment>> task) => appointments);

            return command.Object;
        }
        
        [Fact]
        public void ShouldReturnAppointmentListWhenIsRequestedByUserIdAndCenterId()
        {
            // Arrange
            var command = SetupMock();

            IAppointmentRepository appointmentRepository =
                new AppointmentRepository(command);

            // Act
            var appointments = appointmentRepository.GetUserAndCenterAppointments(1,1);

            // Assert
            Assert.NotEmpty(appointments);
            Assert.True(appointments.Count==4);
        }
    }
}
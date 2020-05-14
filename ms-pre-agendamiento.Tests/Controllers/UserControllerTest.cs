using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ms_pre_agendamiento.Controllers;
using ms_pre_agendamiento.Dto;
using ms_pre_agendamiento.Models;
using ms_pre_agendamiento.Models.Mappers;
using ms_pre_agendamiento.Repository;
using ms_pre_agendamiento.Service;
using Xunit;

namespace ms_pre_agendamiento.Tests.Controllers
{
    public class UserControllerTest
    {
        private UserController SetupUserController(User user)
        {
            var userRepository = new Mock<IUserRepository>();
            var appointmentRepository = new Mock<IAppointmentRepository>();
            
            userRepository.Setup((u) => u.GetById(1)).Returns(() => user);
            userRepository.Setup((u) => u.GetUserAppointmentsById(1)).Returns(() => user);
            appointmentRepository.Setup((a) => a.GetUserAndCenterAppointments(1,1)).Returns(() => user.Appointments);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });
            
            return new UserController(userRepository.Object, new Mapper(config), appointmentRepository.Object);
        }

        [Fact]
        public void ShouldReturnNotFoundWhenUserIsNotValid()
        {
            var userController = SetupUserController(null);
            var result = userController.Get(2);

            Assert.Equal(new NotFoundResult().ToString(), result.ToString());
        }

        [Fact]
        public void ShouldReturnOkWhenUserIdIsValid()
        {
            var user = new User
                {Id = "1", Name = "name", Password = "password"};
            var userController = SetupUserController(user);
            var result = userController.Get(1);
            var objectResult = result as ObjectResult;
            var objectResultValue = objectResult?.Value as User;

            Assert.Equal(StatusCodes.Status200OK, objectResult?.StatusCode);
            Assert.Equal(user.Id, objectResultValue?.Id);
            Assert.Equal(user.Name, objectResultValue?.Name);
        }

        [Fact]
        public void ShouldReturnUserAppointmentListWhenUserIdIsValid()
        {
            var user = new User
                {Id = "1", Name = "name", Password = "password"};
            List<Appointment> appointments = new List<Appointment>();
            appointments.Add(new Appointment() {Id = 1, Hour = new TimeSpan(), Description = "description", 
                HealthcareFacilityId = 1, Type = "userAppointment"});
            user.Appointments = appointments;
            var userController = SetupUserController(user);
            
            var result = userController.GetUserAppointments(1);
            var objectResult = result as ObjectResult;
            var objectResultValue = objectResult?.Value as List<AppointmentResponse>;

            Assert.Equal(StatusCodes.Status200OK, objectResult?.StatusCode);
            Assert.NotNull(objectResultValue);
            Assert.NotEmpty(objectResultValue);
            Assert.Single(objectResultValue);
            AppointmentResponse appointment = objectResultValue.FirstOrDefault();
            Assert.Equal(1, appointment?.Id);
            Assert.Equal("userAppointment", appointment?.Type);
        }
        
        [Fact]
        public void ShouldReturn404WhenUserIdIsNotValid()
        {    

            var userController = SetupUserController(null);
            
            var result = userController.GetUserAppointments(2);
            Assert.Equal(new NotFoundResult().ToString(), result.ToString());
        }
        
        [Fact]
        public void ShouldReturnUserAndCentersAppointmentListWhenUserIdIsValid()
        {
            var user = new User
                {Id = "1", Name = "name", Password = "password"};
            List<Appointment> appointments = new List<Appointment>();
            appointments.Add(new Appointment() {Id = 1, Hour = new TimeSpan(), Description = "description", 
                HealthcareFacilityId = 1, Type = "userAppointment"});
            user.Appointments = appointments;
            var userController = SetupUserController(user);
            
            var result = userController.GetUserAndCenterAppointments(1,1);
            var objectResult = result as ObjectResult;
            var objectResultValue = objectResult?.Value as List<AppointmentResponse>;

            Assert.Equal(StatusCodes.Status200OK, objectResult?.StatusCode);
            Assert.NotNull(objectResultValue);
            Assert.NotEmpty(objectResultValue);
            Assert.Single(objectResultValue);
            AppointmentResponse appointment = objectResultValue.FirstOrDefault();
            Assert.Equal(1, appointment?.Id);
            Assert.Equal("userAppointment", appointment?.Type);
        }
        
        [Fact]
        public void ShouldReturn404WhenUserIdIsNotValidOnGetUserAndCenterAppointments()
        {    

            var userController = SetupUserController(null);
            
            var result = userController.GetUserAndCenterAppointments(2,2);
            Assert.Equal(new NotFoundResult().ToString(), result.ToString());
        }
        
    }
}
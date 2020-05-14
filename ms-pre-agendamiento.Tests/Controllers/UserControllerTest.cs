using System;
using System.Collections.Generic;
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
            var userService = new Mock<IUserService>();
            
            userRepository.Setup((u) => u.GetById(1)).Returns(() => user);
            userRepository.Setup((u) => u.GetUserAppointmentsById(1)).Returns(() => user);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });
            
            return new UserController(userRepository.Object, new Mapper(config), userService.Object);
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
            appointments.Add(new Appointment() {Id = 1, Hour = new TimeSpan(), Description = "description", HealthcareFacilityId = 1});
            user.Appointments = appointments;
            var userController = SetupUserController(user);
            
            var result = userController.GetUserAppointments(1);
            var objectResult = result as ObjectResult;
            var objectResultValue = objectResult?.Value as UserResponse;

            Assert.Equal(StatusCodes.Status200OK, objectResult?.StatusCode);
            Assert.Equal(user.Id, objectResultValue?.Id);
            Assert.Equal(user.Name, objectResultValue?.Name);
            Assert.NotEmpty(user.Appointments);
            Assert.Single(user.Appointments);
        }
        
        [Fact]
        public void ShouldReturn404WhenUserIdIsNotValid()
        {    

            var userController = SetupUserController(null);
            
            var result = userController.GetUserAppointments(2);
            Assert.Equal(new NotFoundResult().ToString(), result.ToString());
        }
        
    }
}
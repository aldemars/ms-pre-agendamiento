using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using ms_pre_agendamiento.Controllers;
using ms_pre_agendamiento.Dto;
using ms_pre_agendamiento.Models;
using ms_pre_agendamiento.Models.Mappers;
using ms_pre_agendamiento.Repository;
using Xunit;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace ms_pre_agendamiento.Tests.Controllers
{
    public class LoginControllerTest
    {
        private LoginController SetupLoginController(User user, LoginRequest loginRequest)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            var config = new MapperConfiguration(cfg =>
            {
                // Add all profiles in current assembly
                cfg.AddProfile<MapperProfile>();
            });

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup((u) => u.GetUser(loginRequest)).Returns(() => user);
            IMapper mapper = new Mapper(config);
            return new LoginController(userRepository.Object, configurationBuilder.Build(), mapper);
        }

        [Fact]
        public void ShouldReturnUnauthorizedWhenUserIsNotValid()
        {
            var loginRequest = new LoginRequest {Name = "testName", Password = "password"};
            var loginController = SetupLoginController(null, loginRequest);
            var result = loginController.Login(loginRequest);

            Assert.Equal(new UnauthorizedResult().ToString(), result.ToString());
        }

        [Fact]
        public void ShouldReturnSuccessWhenLoginUserIsValid()
        {
            var loginRequest = new LoginRequest {Name = "testName", Password = "password"};

            var user = new User
                {Id = "1", Name = loginRequest.Name, Password = loginRequest.Password, Role = "scheduler"};

            var loginController = SetupLoginController(user, loginRequest);
            var result = loginController.Login(loginRequest);
            var objectResult = result as ObjectResult;
            var objectResultValue = objectResult?.Value as LoginResponse;

            Assert.Equal(StatusCodes.Status200OK, objectResult?.StatusCode);
            Assert.Equal(user.Id, objectResultValue?.Id);
            Assert.Equal(user.Name, objectResultValue?.Name);
            Assert.NotNull(objectResultValue?.Token);
        }

        [Fact]
        public void ShouldValidationReturnFalseWhenPasswordMissing()
        {
            var loginRequest = new LoginRequest {Name = "testName"};
            var validationResults = new List<ValidationResult>();
            // Act
            var actual = Validator.TryValidateObject(loginRequest, new ValidationContext(loginRequest),
                validationResults, true);

            // Assert
            Assert.False(actual);
            Assert.True(validationResults.Count > 0);
            Assert.Equal("The Password field is required.", validationResults[0].ErrorMessage);
        }
    }
}
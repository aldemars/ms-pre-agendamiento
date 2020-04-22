
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using ms_pre_agendamiento.Controllers;
using ms_pre_agendamiento.Dto;
using ms_pre_agendamiento.Models;
using ms_pre_agendamiento.Repository;
using Xunit;

namespace ms_pre_agendamiento.Tests.Controllers
{
    
    public class LoginControllerTest
    {
        
        private string APP_SETTING = "AppSettings";
        
        //new User{Id = "1", Name = loginRequest.Name, Password = loginRequest.Password}
        [Fact]
        public void ShouldReturnUnauthorizedWhenUserIsNotValid()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            
            var loginRequest = new LoginRequest {Name="testName", Password="testPassword"};
            var userRepository = new Mock<IUserRepository>();

            userRepository.Setup((u) => u.GetUser(loginRequest)).Returns( () => null);
            var loginController = new LoginController(userRepository.Object,configurationBuilder.Build());
            var result = loginController.Login(loginRequest);
            
            Assert.Equal(new UnauthorizedResult().ToString(),result.ToString());
        }
    }
}
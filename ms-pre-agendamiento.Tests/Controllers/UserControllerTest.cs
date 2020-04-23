using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ms_pre_agendamiento.Controllers;
using ms_pre_agendamiento.Models;
using ms_pre_agendamiento.Repository;
using Xunit;

namespace ms_pre_agendamiento.Tests.Controllers
{
    public class UserControllerTest
    {
        private UserController SetupUserController(User user)
        {
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup((u) => u.GetById(1)).Returns(() => user);
            return new UserController(userRepository.Object);
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
            User user = new User
                {Id = "1", Name = "name", Password = "password"};
            var userController = SetupUserController(user);
            var result = userController.Get(1);
            var objectResult = result as ObjectResult;
            var objectResultValue = objectResult?.Value as User;

            Assert.Equal(StatusCodes.Status200OK, objectResult?.StatusCode);
            Assert.Equal(user.Id, objectResultValue?.Id);
            Assert.Equal(user.Name, objectResultValue?.Name);
        }
    }
}
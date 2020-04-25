using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using ms_pre_agendamiento.Dto;
using Newtonsoft.Json;
using Xunit;

namespace ms_pre_agendamiento.Tests.Integration
{
    public class LoginControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public LoginControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/login")]
        public async Task Login_ShouldReturnUnAuthorizeWhenUserIsInvalid(string url)
        {
            // Arrange
            var client = _factory.CreateClient();
            const string payload = "{\"name\": \"name\",\"password\": \"password\"}";
            HttpContent httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
            
            // Act
            var response = await client.PostAsync(url,httpContent);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", 
                response.Content.Headers.ContentType.ToString());
            var result = response.Content.ReadAsStringAsync().Result;
            var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(result);
            Assert.NotNull(loginResponse.Token);
        } 
            
    }
}
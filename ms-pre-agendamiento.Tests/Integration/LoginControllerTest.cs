using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using ms_pre_agendamiento.Dto;
using Newtonsoft.Json;
using Xunit;

namespace ms_pre_agendamiento.Tests.Integration
{
    public class LoginControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public LoginControllerTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ShouldReturnOKResponseWhenUserIsvalid()
        {
            // Arrange
            var client = _factory.CreateClient();

            var payload = "{\"name\": \"Gonzalo\",\"password\": \"password\"}";

            HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
            // Act
            var response = await client.PostAsync("/Login",c);

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
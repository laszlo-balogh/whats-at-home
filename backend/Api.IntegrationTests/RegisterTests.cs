using Application.Registration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace Api.IntegrationTests
{
    public class RegisterTests : IClassFixture<ApiFactory>
    {        
        private readonly HttpClient _client;
        public RegisterTests(ApiFactory apiFactory)
        {
            _client = apiFactory.CreateClient();            
        }

        [Fact]
        public async Task RegisterNewUserTest()
        {
            // Arrange
            var request = new 
            {
                Email = "test@example.com",
                Password = "Password123!",
                DisplayName = "Test User"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/register", request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

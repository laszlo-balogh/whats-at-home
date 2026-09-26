using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace Api.IntegrationTests
{
    public class LoginTests : IClassFixture<ApiFactory>, IAsyncLifetime
    {
        private readonly HttpClient _client;
        private string? _email;
        private string? _password;
        private const string ErrorMessage = "Invalid email or password.";        

        public LoginTests(ApiFactory apiFactory)
        {
            _client = apiFactory.CreateClient();
        }

        [Fact]
        public async Task LoginWithValidCredentialsTest()
        {
            // Arrange
            var request = new
            {
                Email = _email,
                Password = _password
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/login", request);
            var body = await response.Content.ReadFromJsonAsync<LoginResponse>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(body);

            var parts = body.Token.Split('.');
            Assert.Equal(3, parts.Length);
        }

        [Fact]        
        public async Task LoginWithInvalidEmailTest()
        {
            // Arrange
            var request = new
            {
                Email = "wrongemail@example.com",
                Password = _password
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/login", request);            

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

            var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            Assert.NotNull(problem);
            Assert.Equal(ErrorMessage, problem.Detail);
        }

        [Fact]
        public async Task LoginWithInvalidPasswordTest()
        {
            // Arrange
            var request = new
            {
                Email = _email,
                Password = "WrongPassword123"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/login", request);            

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

            var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            Assert.NotNull(problem);
            Assert.Equal(ErrorMessage, problem.Detail);
        }

        [Fact]
        public async Task LoginWithInvalidEmailAndPasswordTest()
        {
            // Arrange
            var request = new
            {
                Email = "wrongemail@example.com",
                Password = "WrongPassword123!"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/login", request);            

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            
            var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            Assert.NotNull(problem);
            Assert.Equal(ErrorMessage, problem.Detail);
        }

        public async Task InitializeAsync()
        {
            _email = TestData.GenerateRandomEmail();
            _password = TestData.TestPassword;

            var request = new
            {
                Email = _email,
                Password = _password,
                DisplayName = "Test User"
            };

            var response = await _client.PostAsJsonAsync("/api/auth/register", request);

            response.EnsureSuccessStatusCode();            
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        private record LoginResponse(string Token);
    }
}

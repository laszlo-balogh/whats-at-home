using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace Api.IntegrationTests
{
    public class RegisterTests : IClassFixture<ApiFactory>
    {
        private readonly ApiFactory _apiFactory;
        private readonly HttpClient _client;
        public RegisterTests(ApiFactory apiFactory)
        {
            _apiFactory = apiFactory;
            _client = apiFactory.CreateClient();
        }

        [Fact]
        public async Task RegisterNewUserTest()
        {
            // Arrange
            var request = new 
            {
                Email = TestData.GenerateRandomEmail(),
                Password = TestData.TestPassword,
                DisplayName = "Test User"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/register", request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("Nam")]
        [InlineData("InvalidInvalidInvalidInvalid_Dn")]
        [InlineData("")]
        public async Task RegisterUserInvalidDisplayName(string displayName)
        {
            // Arrange
            var request = new
            {
                Email = TestData.GenerateRandomEmail(),
                Password = TestData.TestPassword,
                DisplayName = displayName
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/register", request);
            var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotNull(problem);
            Assert.Contains("DisplayName", problem.Errors.Keys);
        }

        [Fact]
        public async Task RegisterTwoUsersWithSameEmailTest()
        {
            // Arrange
            var request1 = new
            {
                Email = TestData.GenerateRandomEmail(),
                Password = TestData.TestPassword,
                DisplayName = "Test User"
            };

            var request2 = new
            {
                Email = request1.Email,
                Password = TestData.TestPassword,
                DisplayName = "Test User"
            };

            // Act
            var response1 = await _client.PostAsJsonAsync("/api/auth/register", request1);
            var response2 = await _client.PostAsJsonAsync("/api/auth/register", request2);
            var problem = await response2.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response1.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response2.StatusCode);
            Assert.NotNull(problem);
            Assert.Contains("Email", problem.Errors.Keys);

        }

        [Theory]        
        [InlineData("Ab1!x")]
        [InlineData("Password!")]
        [InlineData("Password1")]
        [InlineData("password1!")]
        [InlineData("PASSWORD1!")]        
        public async Task RegisterWeakPasswordTest(string password)
        {
            // Arrange
            var request = new
            {
                Email = TestData.GenerateRandomEmail(),
                Password = password,
                DisplayName = "Test User"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/register", request);
            var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

            // Assert            
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotNull(problem);
            Assert.Contains("Password", problem.Errors.Keys);
        }

        [Fact]
        public async Task RegisterNewUserPersistsAppUserAndStorageTest()
        {
            // Arrange
            var request = new
            {
                Email = TestData.GenerateRandomEmail(),
                Password = TestData.TestPassword,
                DisplayName = "Test User"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/register", request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // A fresh scope gives a fresh DbContext, so we see what was actually committed
            using var scope = _apiFactory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var identityUser = await dbContext.Users.SingleAsync(u => u.Email == request.Email);

            // Looking the AppUser up by the Identity user's Id also verifies the Identity <-> Domain link
            var appUser = await dbContext.AppUsers
                .Include(u => u.Storage)
                .SingleAsync(u => u.Id == identityUser.Id);

            Assert.Equal(request.DisplayName, appUser.DisplayName);
            Assert.NotNull(appUser.Storage);
            Assert.Equal($"{request.DisplayName}'s Storage", appUser.Storage.Name);
            Assert.Equal(identityUser.Id, appUser.Storage.AppUserId);
        }

        [Fact]
        public async Task RegisterWithWeakPasswordDoesNotPersistAnythingTest()
        {
            // Arrange
            // Unique DisplayName, so we can look for a leftover AppUser even without an Identity user Id
            var request = new
            {
                Email = TestData.GenerateRandomEmail(),
                Password = "Password1",
                DisplayName = $"User {Guid.NewGuid().ToString()[..8]}"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/register", request);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            using var scope = _apiFactory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            Assert.False(await dbContext.Users.AnyAsync(u => u.Email == request.Email));
            Assert.False(await dbContext.AppUsers.AnyAsync(u => u.DisplayName == request.DisplayName));
            Assert.False(await dbContext.Storages.AnyAsync(s => s.Name == $"{request.DisplayName}'s Storage"));
        }
    }
}

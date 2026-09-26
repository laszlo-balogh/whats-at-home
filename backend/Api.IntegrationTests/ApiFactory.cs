using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace Api.IntegrationTests
{
    public class ApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {   
        private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder("postgres:17").Build();
        public const string JwtKey = "It_is_a_test_key_for_testing_123";        

        public async Task InitializeAsync()
        { 
            await _postgreSqlContainer.StartAsync();
            
            using var scope = Services.CreateScope();
            await scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.MigrateAsync();

        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            builder.UseSetting("ConnectionStrings:Default", _postgreSqlContainer.GetConnectionString());
            builder.UseSetting("Jwt:Issuer", "test-issuer");
            builder.UseSetting("Jwt:Audience", "test-audience");
            builder.UseSetting("Jwt:Key", JwtKey);
            builder.UseSetting("Jwt:AccessTokenExpirationMinutes", "15");
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await _postgreSqlContainer.DisposeAsync();
            await base.DisposeAsync();            
        }
    }
}

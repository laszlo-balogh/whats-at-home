using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        
        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(string email, string password)
        {
            var user = new ApplicationUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, password);
            
            if (result.Succeeded)
            {
                return (Result.Success(), user.Id);
            }
            else
            {
                var errors = result.Errors.Select(e => (MapErrorCodeToField(e.Code), e.Description));
                return (Result.Failure(errors), string.Empty);
            }
        }
        private static string MapErrorCodeToField(string code) => code switch
        {
            _ when code.StartsWith("Password") => "Password",
            _ when code.Contains("UserName") || code.Contains("Email") => "Email",
            _ => string.Empty
        };

        public async Task<(Result Result, string UserId)> ValidateCredentialsAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return (Result.Failure(new[] { ("Credentials", "Invalid email or password.") }), string.Empty);

            var isMatch = await _userManager.CheckPasswordAsync(user, password);
            if (!isMatch) return (Result.Failure(new[] { ("Credentials", "Invalid email or password.") }), string.Empty);

            return (Result.Success(), user.Id);
        }
    }
}
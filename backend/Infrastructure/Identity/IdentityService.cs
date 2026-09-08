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
                var errors = result.Errors.Select(e => e.Description);
                return (Result.Failure(errors), string.Empty);
            }
        }
    }
}
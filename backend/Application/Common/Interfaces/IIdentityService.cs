using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<(Result Result, string UserId)> CreateUserAsync(string email, string password);
    }
}
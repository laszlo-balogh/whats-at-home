
namespace Application.Common.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(string userId, string email);
    }
}

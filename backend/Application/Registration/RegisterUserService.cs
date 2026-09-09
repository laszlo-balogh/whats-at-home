using Application.Common.Interfaces;
using Application.Common.Models;

namespace Application.Registration
{
    public class RegisterUserService
    {
        private readonly IIdentityService _identityService;

        public RegisterUserService(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result> RegisterUserAsync(RegisterUserRequest request)
        {
            var (result, userId) = await _identityService.CreateUserAsync(request.Email, request.Password);

            if (!result.IsSuccess)
            {
                return Result.Failure(result.Errors);
            }

            // Additional logic for successful registration can be added here

            return Result.Success();
        }
    }
}
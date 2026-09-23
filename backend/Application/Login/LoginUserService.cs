using Application.Common.Interfaces;
using Application.Common.Models;
using FluentValidation;

namespace Application.Login
{
    public class LoginUserService
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;
        private readonly IValidator<LoginUserRequest> _validator;

        public LoginUserService(IIdentityService identityService, ITokenService tokenService, IValidator<LoginUserRequest> validator)
        {
            _identityService = identityService;
            _tokenService = tokenService;
            _validator = validator;
        }

        public async Task<(Result Result, string Token)> LoginAsync(LoginUserRequest request, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            
            if (!validationResult.IsValid)
            {
                return (Result.Failure(validationResult.Errors.Select(e => (e.PropertyName, e.ErrorMessage))), string.Empty);
            }
           
            var (result, userId) = await _identityService.ValidateCredentialsAsync(request.Email, request.Password);
            
            if (!result.IsSuccess)
            {
                return (Result.Failure(result.Errors), string.Empty);
            }

            return (Result.Success(), _tokenService.GenerateAccessToken(userId, request.Email));
        }
    }
}

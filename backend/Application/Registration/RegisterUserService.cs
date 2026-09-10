using Application.Common.Interfaces;
using Application.Common.Models;
using Domain;
using FluentValidation;

namespace Application.Registration
{
    public class RegisterUserService
    {
        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _appDbContext;
        private readonly IValidator<RegisterUserRequest> _validator;

        public RegisterUserService(IIdentityService identityService, IAppDbContext appDbContext, IValidator<RegisterUserRequest> validator)
        {
            _identityService = identityService;
            _appDbContext = appDbContext;
            _validator = validator;
        }

        public async Task<Result> RegisterUserAsync(RegisterUserRequest request, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            await using var transaction = await _appDbContext.BeginTransactionAsync(cancellationToken);

            var (result, userId) = await _identityService.CreateUserAsync(request.Email, request.Password);

            if (!result.IsSuccess)
            {                
                return Result.Failure(result.Errors);
            }

            Storage storage = new Storage
            {                
                Name = $"{request.DisplayName}'s Storage",
                AppUserId = userId
            };

            AppUser newUser = new AppUser
            {
                Id = userId,                
                DisplayName = request.DisplayName,
                Storage = storage
            };

            _appDbContext.Storages.Add(storage);
            _appDbContext.AppUsers.Add(newUser);
            
            await _appDbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return Result.Success();
        }
    }
}
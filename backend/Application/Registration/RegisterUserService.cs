using Application.Common.Interfaces;
using Application.Common.Models;
using Domain;

namespace Application.Registration
{
    public class RegisterUserService
    {
        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _appDbContext;

        public RegisterUserService(IIdentityService identityService, IAppDbContext appDbContext)
        {
            _identityService = identityService;
            _appDbContext = appDbContext;
        }

        public async Task<Result> RegisterUserAsync(RegisterUserRequest request)
        {
            await using var transaction = await _appDbContext.BeginTransactionAsync();

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
            
            await _appDbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return Result.Success();
        }
    }
}
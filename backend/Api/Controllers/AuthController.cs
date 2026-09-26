using Application.Login;
using Application.Registration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly RegisterUserService _registerUserService;
        private readonly LoginUserService _loginUserService;

        public AuthController(RegisterUserService registerUserService, LoginUserService loginUserService)
        {
            _registerUserService = registerUserService;
            _loginUserService = loginUserService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _registerUserService.RegisterUserAsync(request, cancellationToken);

            if (result.IsSuccess) return Ok();

            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(error.Field, error.Message);
            }
            
            return ValidationProblem();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var loginResult = await _loginUserService.LoginAsync(request, cancellationToken);

            if (loginResult.Result.IsSuccess) return Ok(new { token = loginResult.Token });

            return Problem(
                statusCode: StatusCodes.Status401Unauthorized, 
                detail: string.Join(", ", loginResult.Result.Errors.Select(e => e.Message))
                );
        }
    }
}

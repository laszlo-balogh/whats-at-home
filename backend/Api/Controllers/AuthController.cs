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

        public AuthController(RegisterUserService registerUserService)
        {
            _registerUserService = registerUserService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _registerUserService.RegisterUserAsync(request, cancellationToken);

            if (result.IsSuccess) return Ok();

            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(error.Item1, error.Item2);
            }
            

            return ValidationProblem();
        }
    }
}

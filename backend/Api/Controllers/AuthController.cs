using Application.Registration;
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
        public async Task<IActionResult> Register(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _registerUserService.RegisterUserAsync(request, cancellationToken);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}

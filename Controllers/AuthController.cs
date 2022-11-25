using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Badgage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                await authRepository.Register(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

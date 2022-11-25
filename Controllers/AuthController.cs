using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Badgage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;
        private readonly ITokenService tokenService;

        public AuthController(IAuthRepository authRepository, ITokenService tokenService)
        {
            this.authRepository = authRepository;
            this.tokenService = tokenService;
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

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserLogin userLogin)
        {
            var result = await authRepository.Login(userLogin);

            if (result != null)
            {
                string nom = result.Prenom + " " + result.Nom;
                string token = tokenService.GenerateToken(result.IdUtil, nom, result.AdresseMail);

                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}

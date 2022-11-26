using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Badgage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;
        private readonly ITokenService tokenService;
        private readonly ClaimsPrincipal jwt;

        public AuthController(IAuthRepository authRepository, ITokenService tokenService, ClaimsPrincipal jwt)
        {
            this.authRepository = authRepository;
            this.tokenService = tokenService;
            this.jwt = jwt;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                await authRepository.Register(user);
                return Ok("Compte créé");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
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
        
        // pour tester la récupération de l'id via le token jwt
        [Authorize]
        [HttpGet("test")]
        public ActionResult<int> Test()
        {
            return Ok(jwt.FindFirstValue("id"));
        }
    }
}

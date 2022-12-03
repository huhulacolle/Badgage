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
                return Forbid();
            }
        }

        [Authorize]
        [HttpPost("updateMdp")]
        public async Task<IActionResult> UpdateMdp(MdpInput mdpInput)
        {
            try
            {
                int id = int.Parse(jwt.FindFirstValue("id"));
                await authRepository.UpdateMdp(mdpInput, id);
                return Ok();
            }
            catch (Exception e)
            {
                return Forbid(e.Message);
            }
        }
     
    }
}

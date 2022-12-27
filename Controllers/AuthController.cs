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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Exception))]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                await authRepository.Register(user);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        [HttpPut("updateMdp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Exception))]
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
                return Unauthorized(e.Message);
            }
        }

        [HttpPut("forgotMdp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Exception))]
        public async Task<IActionResult> ForgotMdp(UserLogin userLogin)
        {
            try
            {
                await authRepository.ForgotMdp(userLogin);
                return Ok();
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }
    }
}

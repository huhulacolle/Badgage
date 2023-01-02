using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Badgage.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
        {
            var result = await userRepository.GetUsers();
            return Ok(result);
        }

        [HttpGet("Team/{idTeam}")]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsersOnTeam(int idTeam)
        {
            var result = await userRepository.GetUsersOnTeam(idTeam);
            return Ok(result);
        }

        [HttpGet("Email/{Email}")]
        public async Task<ActionResult<UserModel>> GetUser(string Email)
        {
            var result = await userRepository.GetUser(Email);
            return Ok(result);
        }
    }
}

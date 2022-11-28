using Microsoft.AspNetCore.Mvc;

namespace Badgage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository userRepository;
        
        public UserController(IUserRepository userRepository) { 
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var result = await userRepository.GetUsers();
            return Ok(result);
        }

        [HttpGet("{Email}")]
        public async Task<ActionResult<User>> GetUser(string Email)
        {
            var result = await userRepository.GetUser(Email);
            return Ok(result);
        }
    }
}

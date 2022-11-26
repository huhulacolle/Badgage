using Microsoft.AspNetCore.Mvc;

namespace Badgage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<User>> GetUsers()
        {
            return Ok("No data");
        }

        [HttpGet(":Email/:Nom")]
        public async Task<ActionResult<User>> GetUser(string? Email, string? Nom)
        {
            return Ok("No data found with the specified criterias");
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(User user)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

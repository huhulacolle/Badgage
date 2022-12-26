using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Badgage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {


        [HttpGet]
        public async Task<ActionResult<Models.Task>> GetTask()
        {
            return Ok("No Task");
        }


    }
}

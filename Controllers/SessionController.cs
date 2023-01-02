using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Badgage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionRepository sessionRepository;

        public SessionController(ISessionRepository sessionRepository)
        {
            this.sessionRepository = sessionRepository;
        }

        [HttpPost]
        public async Task<IActionResult> SetSession(SessionInput sessionInput)
        {
            try
            {
                await sessionRepository.SetSession(sessionInput);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("User/{idUser}")]
        public async Task<ActionResult<IEnumerable<SessionModel>>> GetSessionByIdUser(int idUser)
        {
            var result = await sessionRepository.GetSessionsByIdUser(idUser);
            return Ok(result);
        }

        [HttpGet("Team/{idTeam}")]
        public async Task<ActionResult<IEnumerable<SessionModel>>> GetSessionByIdTeam(int idTeam)
        {
            var result = await sessionRepository.GetSessionsByIdTask(idTeam);
            return Ok(result);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Badgage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionRepository sessionRepository;
        private readonly ClaimsPrincipal jwt;

        public SessionController(ISessionRepository sessionRepository, ClaimsPrincipal jwt)
        {
            this.sessionRepository = sessionRepository;
            this.jwt = jwt;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Exception))]
        public async Task<IActionResult> SetSession(SessionInput sessionInput)
        {
            try
            {
                var session = new SessionModel()
                {
                    IdUser = int.Parse(jwt.FindFirstValue("id")),
                    IdTask = sessionInput.IdTask,
                    DateDebut = sessionInput.DateDebut,
                    DateFin = sessionInput.DateFin,
                };
                await sessionRepository.SetSession(session);
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

        [HttpGet("Task/{idTask}")]
        public async Task<ActionResult<IEnumerable<SessionModel>>> GetSessionByIdTask(int idTask)
        {
            var result = await sessionRepository.GetSessionsByIdTask(idTask);
            return Ok(result);
        }
    }
}

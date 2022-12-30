using Badgage.Interfaces.Repositories;
using Badgage.Models;
using Badgage.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Badgage.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository taskRepository;
        private readonly ClaimsPrincipal jwt;

        public TaskController(ITaskRepository taskRepository, ClaimsPrincipal jwt)
        {
            this.taskRepository = taskRepository;
            this.jwt = jwt;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasksByUser()
        {
            int idUser = int.Parse(jwt.FindFirstValue("id"));

            var result = await taskRepository.GetTasksByUser(idUser);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Exception))]
        public async Task<IActionResult> SetTask(TaskModel taskModel)
        {
            try
            {
                int idUser = int.Parse(jwt.FindFirstValue("id"));

                bool verif = await taskRepository.VerifUserOnProject(taskModel.IdProjet, idUser);

                if (verif)
                {
                    await taskRepository.SetTask(taskModel, idUser);
                    return StatusCode(201);
                }
                return Unauthorized();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Join")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Exception))]
        public async Task<IActionResult> JoinTask(int idTask)
        {
            try
            {
                int IdUser = int.Parse(jwt.FindFirstValue("id"));
                await taskRepository.SetUserOnTask(new UserOnTaskModel() { IdUser = IdUser, IdTask = idTask });
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

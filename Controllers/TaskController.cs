using Badgage.Interfaces.Repositories;
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
            int id = int.Parse(jwt.FindFirstValue("id"));

            var result = await taskRepository.GetTasksByUser(id);
            return Ok(result);
        }

        [HttpGet("{idTask}")]
        public async Task<ActionResult<TaskModel>> GetTask(int idRole)
        {
            var result = await taskRepository.GetTaskById(idRole);
            return Ok(result);
        }

        [HttpGet("Project/{id}")]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTaskFromProject(int id)
        {
            var result = await taskRepository.GetTaskFromProject(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Exception))]
        public async Task<IActionResult> SetTask(TaskModel taskModel)
        {
            try
            {
                await taskRepository.SetTask(taskModel);
                return Ok("tâche créé");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{idTask}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Exception))]
        public async Task<IActionResult> DeleteTask(int idTask)
        {
            try
            {
                await taskRepository.DeleteTask(idTask);
                return Ok("Tâche supprimé");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

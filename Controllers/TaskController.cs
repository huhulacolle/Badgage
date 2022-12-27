using Badgage.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Badgage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            this._taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasks()
        {
            var result = await _taskRepository.GetTasks();
            return Ok(result);
        }

        [HttpGet("{idTask}")]
        public async Task<ActionResult<Role>> GetTask(int idRole)
        {
            var result = await _taskRepository.GetTask(idRole);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Exception))]
        public async Task<IActionResult> SetTask(TaskModel taskModel)
        {
            try
            {
                await _taskRepository.SetTask(taskModel);
                return Ok("tâche créé");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{idRole}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Exception))]
        public async Task<IActionResult> DeleteTask(int idTask)
        {
            try
            {
                await _taskRepository.DeleteTask(idTask);
                return Ok("Tâche supprimé");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

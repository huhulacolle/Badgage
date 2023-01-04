using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Récupérer un ticket via le Bearer Token de l'utilisateur
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasksByUser()
        {
            int idUser = int.Parse(jwt.FindFirstValue("id"));

            var result = await taskRepository.GetTasksByUser(idUser);
            return Ok(result);
        }

        /// <summary>
        /// Met à jour le nom du ticket
        /// </summary>
        /// <param name="idTask"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Exception))]
        public async Task<IActionResult> UpdateTaskName(int idTask, string name)
        {
            try
            {
                await taskRepository.UpdateTaskName(name, idTask);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Met à jour de la date d'écheance
        /// </summary>
        /// <param name="idTask"></param>
        /// <param name="DateFin"></param>
        /// <returns></returns>
        [HttpPut("DateFin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Exception))]
        public async Task<IActionResult> UpdateTimeEndTask(int idTask, DateTime DateFin)
        {
            try
            {
                await taskRepository.UpdateTimeEndTask(idTask, DateFin);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Récupère des ticket via l'id de l'utilisateur
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        [HttpGet("User/{IdUser}")]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasksByIdUser(int idUser)
        {
            var result = await taskRepository.GetTasksByUser(idUser);
            return Ok(result);
        }

        /// <summary>
        /// Récupère des ticket via l'idProject
        /// </summary>
        /// <param name="idProject"></param>
        /// <returns></returns>
        [HttpGet("Project/{idProject}")]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTaskFromProject(int idProject)
        {
            var result = await taskRepository.GetTaskFromProject(idProject);
            return Ok(result);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Exception))]
        [HttpDelete("{idTask}")]
        public async Task<IActionResult> DeleteTask(int idTask)
        {
            try
            {
                await taskRepository.DeleteTask(idTask);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// <summary>
        /// Récupère la liste des utilisateurs associer au ticket via idTask
        /// </summary>
        /// <param name="idTask"></param>
        /// <returns></returns>
        [HttpGet("ListUser/{idTask}")]
        public async Task<ActionResult<IEnumerable<UserOnTaskModelWithName>>> GetListUserByIdTask(int idTask)
        {
            var result = await taskRepository.GetListUserByIdTask(idTask);
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
                    await taskRepository.SetTask(taskModel);
                    return StatusCode(201);
                }
                return Unauthorized();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Assigner un utilisateur à une tâche
        /// </summary>
        /// <param name="idTask"></param>
        /// <param name="idUser"></param>
        /// <returns></returns>
        [HttpPost("Join")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Exception))]
        public async Task<IActionResult> JoinTask(int idTask, int idUser)
        {
            try
            {
                await taskRepository.SetUserOnTask(new UserOnTaskModel() { IdUser = idUser, IdTask = idTask });
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

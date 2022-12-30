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

        [HttpPost]
        public async Task<IActionResult> SetTask(TaskModel taskModel)
        {
            try
            {
                int idUser = int.Parse(jwt.FindFirstValue("id"));

                var verif = taskRepository.VerifUserOnProject(taskModel.IdProjet, idUser);

                return Ok(verif);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

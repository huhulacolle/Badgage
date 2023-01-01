using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Badgage.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository projectRepository;
        private readonly ClaimsPrincipal jwt;

        public ProjectController(IProjectRepository projectRepository, ClaimsPrincipal jwt)
        {
            this.projectRepository = projectRepository;
            this.jwt = jwt;
        }

        [HttpGet("{idTeam}")]
        public async Task<ActionResult<IEnumerable<ProjectModel>>> GetProjectByTeam(int idTeam)
        {
            var result = await projectRepository.GetProjectByTeam(idTeam);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Exception))]
        public async Task<IActionResult> CreateProject(ProjectModel project)
        {
            try
            {
                int idUser = int.Parse(jwt.FindFirstValue("id"));
                var verif = await projectRepository.VerifTeamUser(idUser, project.IdTeam);

                if (verif)
                {
                    project.ByUser = idUser;
                    await projectRepository.CreateProject(project);
                    return Ok("Projet créé");
                }
                return Unauthorized();

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{idProject}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Exception))]
        public async Task<IActionResult> DeleteProject(int idProject)
        {
            try
            {
                await projectRepository.DeleteProject(idProject);
                return Ok("Projet supprimé");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

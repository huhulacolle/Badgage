using Badgage.Repositories;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Récupérer un projet via idTeam
        /// </summary>
        /// <param name="idTeam"></param>
        /// <returns></returns>
        [HttpGet("Team/{idTeam}")]
        public async Task<ActionResult<IEnumerable<ProjectModel>>> GetProjectByTeam(int idTeam)
        {
            var result = await projectRepository.GetProjectByTeam(idTeam);
            return Ok(result);
        }

        /// <summary>
        /// Récupérer un projet via le Bearer Token de l'utilisateur
        /// </summary>
        /// <returns></returns>
        [HttpGet("User")]
        public async Task<ActionResult<IEnumerable<ProjectModel>>> GetProjectByUser()
        {
            int idUser = int.Parse(jwt.FindFirstValue("id"));

            var result = await projectRepository.GetProjectByUser(idUser);
            return Ok(result);
        }

        /// <summary>
        /// Mettre à jour le nom d'un projet
        /// </summary>
        /// <param name="idProject"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Exception))]
        public async Task<IActionResult> UpdateProjectName(int idProject, string name)
        {
            try
            {
                await projectRepository.UpdateProjectName(idProject, name);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{idProject}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Exception))]
        public async Task<IActionResult> DeleteProject(int idProject)
        {
            try
            {
                await projectRepository.DeleteProject(idProject);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

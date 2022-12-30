using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Badgage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository iProjectRepository;

        public ProjectController(IProjectRepository iProjectRepository)
        {
            this.iProjectRepository = iProjectRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectModel>>> GetProjects()
        {
            var result = await iProjectRepository.GetAllProjects();
            return Ok(result);
        }

        [HttpGet("{idProject}")]
        public async Task<ActionResult<ProjectModel>> GetProject(int idProject)
        {
            var result = await iProjectRepository.GetProject(idProject);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Exception))]
        public async Task<IActionResult> CreateProject(ProjectModel project)
        {
            try
            {
                await iProjectRepository.CreateProject(project);
                return Ok("Projet créé");
            }catch(Exception ex)
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
                await iProjectRepository.DeleteProject(idProject);
                return Ok("Projet supprimé");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

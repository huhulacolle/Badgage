﻿using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var result = await iProjectRepository.GetAllProjects();
            return Ok(result);
        }

        [HttpGet("{idProject}")]
        public async Task<ActionResult<Project>> GetProject(int idProject)
        {
            var result = await iProjectRepository.GetProject(idProject);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(Project project)
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

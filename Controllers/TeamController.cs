using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Badgage.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository teamRepository;
        private readonly ClaimsPrincipal jwt;

        public TeamController(ITeamRepository teamRepository, ClaimsPrincipal jwt)
        {
            this.teamRepository = teamRepository;
            this.jwt = jwt;
        }

        /// <summary>
        /// Récupère toutes les teams de l'utilisateur
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamModel>>> GetTeamByUser()
        {
            int idUser = int.Parse(jwt.FindFirstValue("id"));

            var result = await teamRepository.GetTeamByUser(idUser);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Exception))]
        public async Task<IActionResult> SetTeam(TeamModel teamModel)
        {
            try
            {
                teamModel.ByUser = int.Parse(jwt.FindFirstValue("id"));

                await teamRepository.SetTeam(teamModel);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Modifie le nom de l'équipe
        /// </summary>
        /// <param name="idTeam"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Exception))]
        public async Task<IActionResult> UpdateTeamName(int idTeam, string name)
        {
            try
            {
                await teamRepository.UpdateTeamName(name, idTeam);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Assigner un utilisateur à une équipe
        /// </summary>
        /// <param name="userOnTeamModel"></param>
        /// <returns></returns>
        [HttpPost("Join")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Exception))]
        public async Task<IActionResult> JoinTeam(UserOnTeamModel userOnTeamModel)
        {
            try
            {
                int UserTeamId = int.Parse(jwt.FindFirstValue("id"));

                var Verif = await teamRepository.VerifUserBossTeam(new UserOnTeamModel() { IdUser = UserTeamId, IdTeam = userOnTeamModel.IdTeam });

                if (Verif)
                {
                    await teamRepository.SetUserOnTeam(new UserOnTeamModel() { IdUser = userOnTeamModel.IdUser, IdTeam = userOnTeamModel.IdTeam });
                    return Ok();
                }

                return Unauthorized();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Récupère toutes les équipes via idProject
        /// </summary>
        /// <param name="idProject"></param>
        /// <returns></returns>
        [HttpGet("Project/{idProject}")]
        public async Task<ActionResult<IEnumerable<TeamModel>>> GetTeamByIdProject(int idProject)
        {
            var result = await teamRepository.GetTeamByIdProject(idProject);
            return Ok(result);
        }


        [HttpDelete("{idTeam}")]
        public async Task<IActionResult> DeleteTeam(int idTeam)
        {
            try
            {
                await teamRepository.DeleteTeam(idTeam);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

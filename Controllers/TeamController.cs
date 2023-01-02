using Badgage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

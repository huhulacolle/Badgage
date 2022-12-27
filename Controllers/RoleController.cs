using Microsoft.AspNetCore.Mvc;

namespace Badgage.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository iRoleRepository;

        public RoleController(IRoleRepository iRoleRepository)
        {
            this.iRoleRepository = iRoleRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> getRoles()
        {
            var result = await iRoleRepository.getRoles();
            return Ok(result);
        }

        [HttpGet("{idRole}")]
        public async Task<ActionResult<Role>> getRole(int idRole)
        {
            var result = await iRoleRepository.getRole(idRole); ;
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Exception))]
        public async Task<IActionResult> createRole(Role role)
        {
            try
            {
                await iRoleRepository.createRole(role);
                return Ok("Rôle créé");
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{idRole}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Exception))]
        public async Task<IActionResult> deleteRole(int IdRole)
        {
            try
            {
                await iRoleRepository.deleteRole(IdRole);
                return Ok("Role supprimé");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Badgage.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleRepository iRoleRepository;

        public RoleController(IRoleRepository iRoleRepository)
        {
            this.iRoleRepository = iRoleRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Role>> getRoles()
        {
            return await iRoleRepository.getRoles();
        }

        [HttpGet("{idRole}")]
        public async Task<ActionResult<Role>> getRole(int idRole)
        {
            return await iRoleRepository.getRole(idRole);
        }

        [HttpPost]
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

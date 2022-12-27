using Microsoft.AspNetCore.Mvc;

namespace Badgage.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        public Task<IEnumerable<RoleModel>> getRoles();

        public Task<RoleModel> getRole(int idRole);

        public Task createRole(RoleModel role);

        public Task deleteRole(int idRole);
    }
}

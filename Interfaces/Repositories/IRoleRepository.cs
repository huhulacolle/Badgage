using Microsoft.AspNetCore.Mvc;

namespace Badgage.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        public Task<IEnumerable<Role>> getRoles();

        public Task<Role> getRole(int idRole);

        public Task createRole(Role role);

        public Task deleteRole(int idRole);
    }
}

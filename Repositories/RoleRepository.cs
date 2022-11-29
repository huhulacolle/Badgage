using Badgage.Models;
using Microsoft.AspNetCore.Mvc;

namespace Badgage.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DefaultSqlConnectionFactory defaultSqlConnectionFactory;

        public RoleRepository(DefaultSqlConnectionFactory defaultSqlConnectionFactory)
        {
            this.defaultSqlConnectionFactory = defaultSqlConnectionFactory;
        }

        public async Task createRole(Role role)
        {

            string sql = "INSERT INTO role (libelle) VALUES (@libelle)";
            using var connec = defaultSqlConnectionFactory.Create();
            await connec.ExecuteAsync(sql, role);
        }

        public async Task deleteRole(int idRole)
        {
            var roleDic = new Dictionary<string, object>()
            {
                { "@idRole", idRole },
            };
            var paramDic = new DynamicParameters(roleDic);

            string sql = "DELETE FROM role WHERE idRole = @idRole";
            using var connec = defaultSqlConnectionFactory.Create();
            await connec.ExecuteAsync(sql, paramDic);
        }

        public async Task<Role> getRole(int idRole)
        {
            var roleDic = new Dictionary<string, object>()
            {
                { "@idRole", idRole },
            };
            var paramDic = new DynamicParameters(roleDic);

            string sql = "Select * FROM role WHERE idRole = @idRole";
            using var connec = defaultSqlConnectionFactory.Create();
            return await connec.QueryFirstOrDefaultAsync<Role>(sql, paramDic);
        }

        public async Task<IEnumerable<Role>> getRoles()
        {
            string sql = "Select * FROM role";
            using var connec = defaultSqlConnectionFactory.Create();
            return await connec.QueryAsync<Role>(sql);
        }
    }
}

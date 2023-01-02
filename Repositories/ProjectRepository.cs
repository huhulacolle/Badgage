using Badgage.Models;
using Microsoft.AspNetCore.Rewrite;

namespace Badgage.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DefaultSqlConnectionFactory defaultSqlConnectionFactory;

        public ProjectRepository(DefaultSqlConnectionFactory defaultSqlConnectionFactory)
        {
            this.defaultSqlConnectionFactory = defaultSqlConnectionFactory;
        }

        public async Task CreateProject(ProjectModel project)
        {
            using var connec = defaultSqlConnectionFactory.Create();

            string sql = "INSERT INTO project (projectName, idTeam, ByUser) VALUES (@projectName, @idTeam, @ByUser)";
            await connec.ExecuteAsync(sql, project);
        }

        public async Task DeleteProject(int idProject)
        {
            var projectDic = new Dictionary<string, object>()
            {
                { "@idProject", idProject },
            };
            var paramDic = new DynamicParameters(projectDic);
            string sql = "DELETE FROM project WHERE idProject = @idProject";
            using var connec = defaultSqlConnectionFactory.Create();
            await connec.ExecuteAsync(sql, paramDic);
        }

        public async Task<IEnumerable<ProjectModel>> GetProjectByTeam(int idTeam)
        {
            var dictionnary = new Dictionary<string, object>()
            {
                { "@idTeam", idTeam },
            };
            var param = new DynamicParameters(dictionnary);

            string sql = "SELECT project.* FROM project WHERE idTeam = @idTeam";

            using var connec = defaultSqlConnectionFactory.Create();
            return await connec.QueryAsync<ProjectModel>(sql, param);
        }

        public async Task<IEnumerable<ProjectModel>> GetProjectByUser(int idUser)
        {
            var dictionnary = new Dictionary<string, object>()
            {
                { "@idUser", idUser },
            };
            var param = new DynamicParameters(dictionnary);

            string sql = "SELECT project.* FROM project LEFT JOIN teamuser ON teamuser.idTeam = project.idTeam WHERE teamuser.idUser = @idUser";

            using var connec = defaultSqlConnectionFactory.Create();
            return await connec.QueryAsync<ProjectModel>(sql, param);
        }

        public async Task<bool> VerifTeamUser(int idUser, int idTeam)
        {
            var dictionary = new Dictionary<string, object>()
            {
                { "@idUser", idUser },
                { "@idTeam", idTeam }
            };
            var param = new DynamicParameters(dictionary);

            string sqlVerif = "SELECT * FROM TeamUser WHERE idUser = @idUser AND idTeam = @idTeam";
            using var connec = defaultSqlConnectionFactory.Create();
            
            var verif = await connec.QueryFirstOrDefaultAsync(sqlVerif, param);

            if (verif != null)
            {
                return true;
            }
            return false;
        }
    }
}

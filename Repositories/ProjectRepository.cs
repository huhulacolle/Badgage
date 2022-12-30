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
            string sql = "INSERT INTO project (projectName) VALUES (@projectName)";
            using var connec = defaultSqlConnectionFactory.Create();
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

        public async Task<IEnumerable<ProjectModel>> GetProjectsByUser(int idUser)
        {
            string sql = "SELECT project.* FROM project";
            using var connec = defaultSqlConnectionFactory.Create();
            return await connec.QueryAsync<ProjectModel>(sql);
        }
    }
}

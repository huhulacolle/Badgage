namespace Badgage.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        public Task<IEnumerable<ProjectModel>> GetProjectByTeam(int idUser);

        public Task CreateProject(ProjectModel project);

        public Task DeleteProject(int idProject);

        public Task<bool> VerifTeamUser(int idUser, int idTeam);
    }
}

namespace Badgage.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        public Task<IEnumerable<ProjectModel>> GetProjectsByUser(int idUser);

        public Task CreateProject(ProjectModel project);

        public Task DeleteProject(int idProject);
    }
}

namespace Badgage.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        public Task<IEnumerable<ProjectModel>> GetAllProjects();

        public Task<ProjectModel> GetProject(int idProject);

        public Task CreateProject(ProjectModel project);

        public Task DeleteProject(int idProject);
    }
}

namespace Badgage.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        public Task<IEnumerable<Project>> GetAllProjects();

        public Task<Project> GetProject(int idProject);

        public Task CreateProject(Project project);

        public Task DeleteProject(int idProject);
    }
}

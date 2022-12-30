namespace Badgage.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        public Task<IEnumerable<TaskModel>> GetTasksByUser(int id);

        public Task<TaskModel> GetTaskById(int idTask);

        public Task SetTask(TaskModel taskModel);

        public Task DeleteTask(int idTask);

        public Task<IEnumerable<TaskModel>> GetTaskFromProject(int idProject);

        Task VerifUserProject();
    }
}

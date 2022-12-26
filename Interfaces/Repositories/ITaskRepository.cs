namespace Badgage.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        public Task<IEnumerable<TaskModel>> GetTasks();

        public Task<TaskModel> GetTask(int idTask);

        public Task SetTask(TaskModel taskModel);

        public Task DeleteTask(int idTask);
    }
}

namespace Badgage.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        public Task<IEnumerable<TaskModel>> GetTasksByUser(int id);

        public Task<TaskModel> GetTaskById(int idTask);

        public Task SetTask(TaskModel taskModel);

        public Task DeleteTask(int idTask);

        public Task UpdateTimeEndTask(int idTask, DateTime DateFin);

        public Task UpdateTaskName(string name, int idTask);

        public Task<IEnumerable<TaskModel>> GetTaskFromProject(int idProject);

        public Task<bool> VerifUserOnProject(int idProject, int idUser);

        public Task SetUserOnTask(UserOnTaskModel userOnTaskModel);

        public Task<IEnumerable<UserOnTaskModelWithName>> GetListTaskByIdTask(int idTask);
    }
}

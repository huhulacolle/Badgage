using Badgage.Models;

namespace Badgage.Repositories
{
    public class TaskRepository : ITaskRepository
    {

        private readonly DefaultSqlConnectionFactory defaultSqlConnectionFactory;

        public TaskRepository(DefaultSqlConnectionFactory defaultSqlConnectionFactory)
        {
            this.defaultSqlConnectionFactory = defaultSqlConnectionFactory;
        }

        public async Task DeleteTask(int idTask)
        {
            var dictionnary = new Dictionary<string, object>()
            {
                { "@idTask", idTask },
            };
            var parameters = new DynamicParameters(dictionnary);

            string sql = "DELETE FROM Task WHERE idtache = @idTask";
            using var connec = defaultSqlConnectionFactory.Create();
            await connec.ExecuteAsync(sql, parameters);
        }

        public async Task<TaskModel> GetTask(int idTask)
        {
            var dictionnary = new Dictionary<string, object>()
            {
                { "@idTask", idTask },
            };
            var parameters = new DynamicParameters(dictionnary);

            string sql = "Select * FROM role WHERE idtache = @idTask";
            using var connec = defaultSqlConnectionFactory.Create();
            return await connec.QueryFirstOrDefaultAsync<TaskModel>(sql, parameters);
        }

        public async Task<IEnumerable<TaskModel>> GetTasks()
        {
            string sql = "Select * FROM Task";
            using var connec = defaultSqlConnectionFactory.Create();
            return await connec.QueryAsync<TaskModel>(sql);
        }

        public async Task SetTask(TaskModel taskModel)
        {
            string sql = "INSERT INTO Task (nomdetache, description, datefin, datecreation) VALUES (@nomdetache, @description, @datefin, @datecreation)";
            using var connec = defaultSqlConnectionFactory.Create();
            await connec.ExecuteAsync(sql, taskModel);
        }
    }
}

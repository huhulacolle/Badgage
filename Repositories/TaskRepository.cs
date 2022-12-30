using Badgage.Models;
using System.Threading.Tasks;

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

        public async Task<TaskModel> GetTaskById(int idTask)
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

        public async Task<IEnumerable<TaskModel>> GetTasksByUser(int id)
        {
            var dictionnary = new Dictionary<string, object>()
            {
                { "@idUtil", id },
            };
            var parameters = new DynamicParameters(dictionnary);

            string sql = "Select * FROM Task where idUtil = @idUtil";
            using var connec = defaultSqlConnectionFactory.Create();

            return await connec.QueryAsync<TaskModel>(sql, parameters);
        }

        public async Task SetTask(TaskModel taskModel)
        {
            string sql = @"INSERT INTO Task (idProjet, idUtil, nomdetache, description, datefin, datecreation) 
                            VALUES (@idProjet, @idUtil, @nomdetache, @description, @datefin, @datecreation)";

            using var connec = defaultSqlConnectionFactory.Create();
            await connec.ExecuteAsync(sql, taskModel);
        }

        public Task<IEnumerable<TaskModel>> GetTaskFromProject(int idProject)
        {
            var dictionnary = new Dictionary<string, object>()
            {
                { "@idProject", idProject },
            };
            var parameters = new DynamicParameters(dictionnary);

            string sql = "SELECT nomdetache, description, datefin, datecreation FROM task WHERE idProjet = @idProject";

            using var connec = defaultSqlConnectionFactory.Create();
            return connec.QueryAsync<TaskModel>(sql, parameters);
        }
    }
}

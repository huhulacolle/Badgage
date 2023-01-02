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

            string sql = @"DELETE FROM Sessions WHERE idTask = @idTask;
                            DELETE FROM TaskUser WHERE idTask = @idTask;
                            DELETE FROM Task WHERE idTask = @idTask;";
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

            string sql = "SELECT task.* FROM task LEFT JOIN taskuser ON taskuser.idTask = task.idTask WHERE taskuser.idUser = @idUtil";
            using var connec = defaultSqlConnectionFactory.Create();

            return await connec.QueryAsync<TaskModel>(sql, parameters);
        }

        public async Task SetTask(TaskModel taskModel)
        {
            string sql = @"INSERT INTO Task (idProjet, nomdetache, description, datefin, datecreation) 
                            VALUES (@idProjet, @nomdetache, @description, @datefin, @datecreation); SELECT LAST_INSERT_ID()";

            using var connec = defaultSqlConnectionFactory.Create();
            int idTask = await connec.QueryFirstOrDefaultAsync<int>(sql, taskModel);
        }

        public async Task SetUserOnTask(UserOnTaskModel userOnTaskModel)
        {
            string sql = "INSERT INTO TaskUser (idUser, IdTask) VALUES (@idUser, @IdTask)";

            using var connec = defaultSqlConnectionFactory.Create();
            await connec.ExecuteAsync(sql, userOnTaskModel);
        }

        public async Task<IEnumerable<TaskModel>> GetTaskFromProject(int idProject)
        {
            var dictionnary = new Dictionary<string, object>()
            {
                { "@idProject", idProject },
            };
            var parameters = new DynamicParameters(dictionnary);

            string sql = "SELECT nomdetache, description, datefin, datecreation FROM task WHERE idProjet = @idProject";

            using var connec = defaultSqlConnectionFactory.Create();
            return await connec.QueryAsync<TaskModel>(sql, parameters);
        }

        public async Task<bool> VerifUserOnProject(int idProject, int idUser)
        {
            var dictionnary = new Dictionary<string, object>()
            {
                { "@idProject", idProject },
                { "@idUser", idUser },
            };
            var parameters = new DynamicParameters(dictionnary);

            string sql = "SELECT project.* FROM project LEFT JOIN teamuser on teamuser.idTeam = project.idTeam WHERE idProject = @idProject AND teamuser.idUser = @idUser";

            using var connec = defaultSqlConnectionFactory.Create();
            var verif = await connec.QueryFirstOrDefaultAsync(sql, parameters);

            if (verif != null)
            {
                return true;
            }
            return false;
        }
    }
}

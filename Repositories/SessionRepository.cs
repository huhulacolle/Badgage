using System.Threading.Tasks;

namespace Badgage.Repositories
{
    public class SessionRepository : ISessionRepository
    {

        private readonly DefaultSqlConnectionFactory defaultSqlConnectionFactory;

        public SessionRepository(DefaultSqlConnectionFactory defaultSqlConnectionFactory)
        {
            this.defaultSqlConnectionFactory = defaultSqlConnectionFactory;
        }

        public async Task<IEnumerable<SessionModel>> GetSessionsByIdTask(int idTask)
        {
            var dictionnary = new Dictionary<string, object>()
            {
                { "@idTask", idTask },
            };
            var parameters = new DynamicParameters(dictionnary);

            string sql = "SELECT * FROM sessions WHERE idTask = @idTask";

            var connec = defaultSqlConnectionFactory.Create();
            return await connec.QueryAsync<SessionModel>(sql, parameters);
        }

        public async Task<IEnumerable<SessionModel>> GetSessionsByIdUser(int idUser)
        {
            var dictionnary = new Dictionary<string, object>()
            {
                { "@idUser", idUser },
            };
            var parameters = new DynamicParameters(dictionnary);

            string sql = "SELECT * FROM sessions WHERE idUser = @idUser";

            var connec = defaultSqlConnectionFactory.Create();
            return await connec.QueryAsync<SessionModel>(sql, parameters);
        }

        public async Task SetSession(SessionModel sessionModel)
        {
            string sql = "INSERT INTO sessions (idTask, idUser, DateDebut, DateFin) VALUES (@idTask, @idUser, @DateDebut, @DateFin);";

            var connec = defaultSqlConnectionFactory.Create();
            await connec.ExecuteAsync(sql, sessionModel); 
        }
    }
}

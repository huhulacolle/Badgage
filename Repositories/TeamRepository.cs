namespace Badgage.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly DefaultSqlConnectionFactory defaultSqlConnectionFactory;

        public TeamRepository(DefaultSqlConnectionFactory defaultSqlConnectionFactory)
        {
            this.defaultSqlConnectionFactory = defaultSqlConnectionFactory;
        }

        public async Task DeleteTeam(int idTeam)
        {
            var dictionary = new Dictionary<string, object>()
            {
                { "@idTeam", idTeam },
            };
            var param = new DynamicParameters(dictionary);

            string sql = @"DELETE sessions FROM sessions LEFT JOIN task ON task.idTask = sessions.idTask 
                                LEFT JOIN project ON project.idProject = task.idprojet WHERE project.idTeam = @idTeam;

                            DELETE taskuser FROM taskuser LEFT JOIN task ON task.idTask = taskuser.idTask 
                                LEFT JOIN team on team.idTeam = task.idTask WHERE team.idTeam = @idTeam;

                            DELETE task FROM task LEFT JOIN team on team.idTeam = task.idTask WHERE team.idTeam = @idTeam;

                            DELETE teamuser FROM teamuser WHERE idTeam = @idTeam;

                            DELETE FROM team WHERE idTeam = @idTeam;";

            using var connec = defaultSqlConnectionFactory.Create();
            await connec.ExecuteAsync(sql, param);
        }

        public async Task<IEnumerable<TeamModel>> GetTeamByUser(int idUser)
        {
            var dictionary = new Dictionary<string, object>()
            {
                { "@idUser", idUser },
            };
            var param = new DynamicParameters(dictionary);

            string sql = @"SELECT team.*, count(teamuser.idTeam) as NbTeam 
                            FROM team 
                            LEFT JOIN teamuser ON teamuser.idTeam = team.idTeam 
                            WHERE teamuser.idUser = @idUser 
                            GROUP BY teamuser.idTeam";

            using var connec = defaultSqlConnectionFactory.Create();
            return await connec.QueryAsync<TeamModel>(sql, param);
        }

        public async Task SetTeam(TeamModel teamModel)
        {
            string sql = "INSERT INTO Team (nom, byUser) VALUES (@Nom, @ByUser); SELECT LAST_INSERT_ID()";

            using var connec = defaultSqlConnectionFactory.Create();
            int idTeam = await connec.QueryFirstOrDefaultAsync<int>(sql, teamModel);
            await SetUserOnTeam(new UserOnTeamModel() { IdUser = teamModel.ByUser, IdTeam = idTeam });
        }

        public async Task SetUserOnTeam(UserOnTeamModel userOnTeamModel)
        {
            string sql = "INSERT INTO TeamUser (idUser, idTeam) VALUES (@idUser, @IdTeam)";

            using var connec = defaultSqlConnectionFactory.Create();
            await connec.ExecuteAsync(sql, userOnTeamModel);
        }

        public async Task UpdateTeamName(string name, int idTeam)
        {
            var dictionnary = new Dictionary<string, object>()
            {
                { "@idTeam", idTeam },
                { "@name", name },
            };
            var param = new DynamicParameters(dictionnary);

            string sql = "UPDATE team SET nom = @name WHERE idTeam = @idTeam";

            using var connec = defaultSqlConnectionFactory.Create();
            await connec.ExecuteAsync(sql, param);
        }

        public async Task<bool> VerifUserBossTeam(UserOnTeamModel userOnTeamModel)
        {
            string sql = "SELECT * FROM team WHERE byUser = @IdUser and idTeam = @IdTeam";

            using var connec = defaultSqlConnectionFactory.Create();
            var result = await connec.QueryFirstOrDefaultAsync(sql, userOnTeamModel);
            if (result != null)
            {
                return true;
            }
            return false;
        }
    }
}

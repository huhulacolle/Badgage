using Badgage.Models;

namespace Badgage.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly DefaultSqlConnectionFactory defaultSqlConnectionFactory;

        public TeamRepository(DefaultSqlConnectionFactory defaultSqlConnectionFactory)
        {
            this.defaultSqlConnectionFactory = defaultSqlConnectionFactory;
        }

        public async Task<IEnumerable<TeamModel>> GetTeamByUser(int idUser)
        {
            var dictionary = new Dictionary<string, object>()
            {
                { "@idUser", idUser },
            };
            var param = new DynamicParameters(dictionary);

            string sql = "SELECT * FROM team LEFT JOIN teamuser ON teamuser.idTeam = team.idTeam WHERE teamuser.idUser = @idUser";

            using var connec = defaultSqlConnectionFactory.Create();
            return await connec.QueryAsync<TeamModel>(sql, param);
        }

        public async Task SetTeam(TeamModel teamModel)
        {
            string sql = "INSERT INTO Team (nom, byUser) VALUES (@Nom, @ByUser); SELECT LAST_INSERT_ID()";

            using var connec = defaultSqlConnectionFactory.Create();
            int idTeam = await connec.QueryFirstOrDefaultAsync<int>(sql, teamModel);
            await SetUserOnTeam(new UserOnTeamModel() { IdUser = teamModel.ByUser , IdTeam = idTeam });
        }

        public async Task SetUserOnTeam(UserOnTeamModel userOnTeamModel)
        {
            string sql = "INSERT INTO TeamUser (idUser, idTeam) VALUES (@idUser, @IdTeam)";

            using var connec = defaultSqlConnectionFactory.Create();
            await connec.ExecuteAsync(sql, userOnTeamModel);
        }

        public async Task<bool> VerifUserBossTeam(UserOnTeamModel userOnTeamModel)
        {
            string sql = "SELECT * FROM TeamUser WHERE idTeam = @idTeam AND idUser = @IdUser";

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

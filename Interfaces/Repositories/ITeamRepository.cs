namespace Badgage.Interfaces.Repositories
{
    public interface ITeamRepository
    {

        public Task<IEnumerable<TeamModel>> GetTeamByUser(int idUser);

        public Task SetTeam(TeamModel teamModel);

        public Task SetUserOnTeam(UserOnTeamModel userOnTeamModel);

        public Task<bool> VerifUserBossTeam(UserOnTeamModel userOnTeamModel);

        public Task DeleteTeam(int idTeam);

        public Task UpdateTeamName(string name, int idTeam);

        public Task<IEnumerable<TeamModel>> GetTeamByIdProject(int idProject);

    }
}

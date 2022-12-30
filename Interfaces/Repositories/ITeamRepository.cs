namespace Badgage.Interfaces.Repositories
{
    public interface ITeamRepository
    {

        Task<IEnumerable<TeamModel>> GetTeamByUser(int idUser);

        Task SetTeam(TeamModel teamModel);

        Task SetUserOnTeam(UserOnTeamModel userOnTeamModel);

        Task<bool> VerifUserBossTeam(UserOnTeamModel userOnTeamModel);

    }
}

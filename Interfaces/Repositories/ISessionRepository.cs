namespace Badgage.Interfaces.Repositories
{
    public interface ISessionRepository
    {
        public Task SetSession(SessionInput sessionInput);
        public Task<IEnumerable<SessionModel>> GetSessionsByIdUser(int idUser);
        public Task<IEnumerable<SessionModel>> GetSessionsByIdTask(int idTask);
    }
}

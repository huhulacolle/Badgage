using System.Collections;

namespace Badgage.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<UserModel?> GetUser(string? Email);

        public Task<IEnumerable<UserModel>> GetUsersOnTeam(int idTeam);

        public Task<IEnumerable<UserModel>> GetUsers();
    }
}

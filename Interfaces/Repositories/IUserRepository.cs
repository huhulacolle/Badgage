using System.Collections;

namespace Badgage.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User?>> GetUsers();

        public Task<User?> GetUser(string? Email);

    }
}

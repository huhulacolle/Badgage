using System.Collections;

namespace Badgage.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<UserModel?>> GetUsers();

        public Task<UserModel?> GetUser(string? Email);

    }
}

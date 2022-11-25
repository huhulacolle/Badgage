namespace Badgage.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        public Task<string> Login(User user);
        public Task Register(User user);
    }
}

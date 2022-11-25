namespace Badgage.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        public Task<User> Login(UserLogin userLogin);
        public Task Register(User user);
    }
}

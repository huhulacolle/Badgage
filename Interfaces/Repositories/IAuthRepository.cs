namespace Badgage.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        public Task<UserModel?> Login(UserLogin userLogin);
        public Task Register(UserModel user);

        public Task UpdateMdp(MdpInput mdpInput, int id);

        public Task ForgotMdp(UserLogin userLogin);
    }
}

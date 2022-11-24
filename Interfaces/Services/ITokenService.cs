namespace Badgage.Interfaces.Services
{
    public interface ITokenService
    {
        public string GenerateToken(int Id, string Nom, string Email);
    }
}

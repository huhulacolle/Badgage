namespace Badgage.Repositories
{
    using Badgage.Infrastructure;
    using BCrypt.Net;
    public class AuthRepository : IAuthRepository
    {
        private readonly DefaultSqlConnectionFactory defaultSqlConnectionFactory;

        public AuthRepository(DefaultSqlConnectionFactory defaultSqlConnectionFactory)
        {
            this.defaultSqlConnectionFactory = defaultSqlConnectionFactory;
        }
        public Task<string> Login(User user)
        {
            throw new NotImplementedException();
        }

        public async Task Register(User user)
        {
            user.Mdp = BCrypt.HashPassword(user.Mdp);

            string sql = "INSERT INTO user (prenom, nom, datenaiss, adressemail, mdp) VALUES (@Prenom, @Nom, @DateNaiss, @AdresseMail, @Mdp)";

            using var connec = defaultSqlConnectionFactory.Create();
            await connec.ExecuteAsync(sql, user);  
        }
    }
}

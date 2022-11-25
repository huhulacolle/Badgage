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
        public async Task<User?> Login(UserLogin userLogin)
        {
            string sql = "SELECT IdUtil, adressemail, mdp FROM user WHERE adressemail = @adressemail";

            using var connec = defaultSqlConnectionFactory.Create();
            var result = await connec.QueryFirstOrDefaultAsync<User>(sql, userLogin);
            
            if (result != null && BCrypt.Verify(userLogin.Mdp, result.Mdp))
            {
                return result;
            }
            return null;
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

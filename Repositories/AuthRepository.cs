namespace Badgage.Repositories
{
    using Badgage.Exceptions;
    using System.ComponentModel.DataAnnotations;
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
            string sql = "SELECT IdUtil, adressemail, nom, prenom, mdp FROM user WHERE adressemail = @adressemail";

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

            // Première lettre en majuscule
            user.Prenom = char.ToUpper(user.Prenom[0]) + user.Prenom[1..];
            user.Nom = char.ToUpper(user.Nom[0]) + user.Nom[1..];

            var email = new EmailAddressAttribute();
            if(!email.IsValid(user.AdresseMail)) throw new EmailNotValidException();

            string sql = "INSERT INTO user (prenom, nom, datenaiss, adressemail, mdp) VALUES (@Prenom, @Nom, @DateNaiss, @AdresseMail, @Mdp)";

            using var connec = defaultSqlConnectionFactory.Create();
            await connec.ExecuteAsync(sql, user);  
        }
    }
}

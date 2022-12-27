namespace Badgage.Repositories
{
    using System.ComponentModel.DataAnnotations;
    using Badgage.Models;
    using BCrypt.Net;

    public class AuthRepository : IAuthRepository
    {
        private readonly DefaultSqlConnectionFactory defaultSqlConnectionFactory;

        public AuthRepository(DefaultSqlConnectionFactory defaultSqlConnectionFactory)
        {
            this.defaultSqlConnectionFactory = defaultSqlConnectionFactory;
        }

        public async Task<UserModel?> Login(UserLogin userLogin)
        {
            string sql = "SELECT IdUtil, adressemail, nom, prenom, mdp FROM user WHERE adressemail = @adressemail";

            using var connec = defaultSqlConnectionFactory.Create();
            var result = await connec.QueryFirstOrDefaultAsync<UserModel>(sql, userLogin);
            
            if (result != null && BCrypt.Verify(userLogin.Mdp, result.Mdp))
            {
                return result;
            }
            return null;
        }

        public async Task Register(UserModel user)
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

        public async Task UpdateMdp(MdpInput mdpInput, int id)
        {
            var dictionary = new Dictionary<string, object>()
            {
                { "@id", id },
            };
            var param = new DynamicParameters(dictionary);

            string getMdpSql = "SELECT mdp FROM user WHERE idUtil = @id";

            using var connec = defaultSqlConnectionFactory.Create();
            string currentMdp = await connec.QueryFirstOrDefaultAsync<string>(getMdpSql, param);

            if(BCrypt.Verify(mdpInput.OldMdp, currentMdp))
            {
                dictionary = new Dictionary<string, object>()
                {
                    {"@id", id },
                    {"mdp", BCrypt.HashPassword(mdpInput.NewMdp) }
                };
                param = new DynamicParameters(dictionary);

                string newMdpSql = "UPDATE user SET mdp = @mdp WHERE idUtil = @id";
                await connec.ExecuteAsync(newMdpSql, param);
            }
            else
            {
                throw new PasswordDoesNotMatchException();
            }
        }

        // méthode temporaire
        public async Task ForgotMdp(UserLogin userLogin)
        {
            userLogin.Mdp = BCrypt.HashPassword(userLogin.Mdp);

            string sql = "UPDATE user SET mdp = @Mdp WHERE adressemail = @AdresseMail";

            using var connec = defaultSqlConnectionFactory.Create();
            await connec.ExecuteAsync(sql, userLogin);
        }
    }
}

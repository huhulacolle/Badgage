namespace Badgage.Repositories
{

    using Badgage.Exceptions;
    using System.ComponentModel.DataAnnotations;
    using BCrypt.Net;
    using System.Threading.Tasks;
    using Badgage.Models;
    using System.Collections.Generic;
    using System.Collections;

    public class UserRepository : IUserRepository
    {
        private readonly DefaultSqlConnectionFactory defaultSqlConnectionFactory;

        public UserRepository(DefaultSqlConnectionFactory defaultSqlConnectionFactory)
        {
            this.defaultSqlConnectionFactory = defaultSqlConnectionFactory;
        }

        public async Task<User> GetUser(string Email)
        {
            var emailDic = new Dictionary<string, object>()
            {
                { "@email", Email },
            };
            var param = new DynamicParameters(emailDic);

            string sql = "SELECT * FROM user WHERE adressemail = @email";
            using var connec = defaultSqlConnectionFactory.Create();
            var result = await connec.QueryFirstOrDefaultAsync<User>(sql, param);
            return result;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            string sql = "SELECT * FROM user ";
            using var connec = defaultSqlConnectionFactory.Create();
            var result = await connec.QueryAsync<User>(sql);
            return result;
        }

    }
}

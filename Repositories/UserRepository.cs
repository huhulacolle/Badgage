﻿namespace Badgage.Repositories
{
    using Badgage.Models.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserRepository : IUserRepository
    {
        private readonly DefaultSqlConnectionFactory defaultSqlConnectionFactory;

        public UserRepository(DefaultSqlConnectionFactory defaultSqlConnectionFactory)
        {
            this.defaultSqlConnectionFactory = defaultSqlConnectionFactory;
        }

        public async Task<UserModel> GetUser(string Email)
        {
            var emailDic = new Dictionary<string, object>()
            {
                { "@email", Email },
            };
            var param = new DynamicParameters(emailDic);

            string sql = "SELECT * FROM user WHERE adressemail = @email";
            using var connec = defaultSqlConnectionFactory.Create();
            var result = await connec.QueryFirstOrDefaultAsync<UserModel>(sql, param);
            return result;
        }

        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            string sql = "SELECT idUtil, nom, prenom, adresseMail FROM user ";
            using var connec = defaultSqlConnectionFactory.Create();
            return await connec.QueryAsync<UserModel>(sql);
        }

        public async Task<IEnumerable<UserModel>> GetUsersOnTeam(int idTeam)
        {
            var dictionary = new Dictionary<string, object>()
            {
                { "@idTeam", idTeam },
            };
            var param = new DynamicParameters(dictionary);

            string sql = "SELECT idUtil, prenom, nom, datenaiss, adressemail FROM user LEFT JOIN teamuser ON teamuser.idUser = idUtil WHERE idTeam = @idTeam";

            using var connec = defaultSqlConnectionFactory.Create();
            return await connec.QueryAsync<UserModel>(sql, param);
        }
    }
}

using MySql.Data.MySqlClient;
using System.Data;

namespace Badgage.Infrastructure
{
    public class DefaultSqlConnectionFactory
    {
        private string ConnectionString;

        public DefaultSqlConnectionFactory(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IDbConnection Create()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}

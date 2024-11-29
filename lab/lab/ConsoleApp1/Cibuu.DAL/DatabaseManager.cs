using Cibuu.DAL.Utilities;
using System.Linq;
using Npgsql;

namespace Cibuu.DAL
{
    public class DatabaseManager
    {
        private string _connectionString;

        public DatabaseManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public NpgsqlConnection GetConnection()
        {
            var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public void CloseConnection(NpgsqlConnection connection)
        {
            if (connection != null)
            {
                connection.Close();
            }
        }

        public static bool AuthenticateUser(string email, string username, string password)
        {
            using (var context = new CibuuDbContext()) 
            {
                var user = context.Users.FirstOrDefault(u => u.Email == email && u.Username == username);

                if (user == null)
                    return false;

                return PasswordHasher.Verify(password, user.PasswordHash); 
            }
        }
    }
}

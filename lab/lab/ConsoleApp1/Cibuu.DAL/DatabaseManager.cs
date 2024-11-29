using Cibuu.DAL.Utilities;
using Cibuu.DAL.models; 
using System.Linq;
using Npgsql;

namespace Cibuu.DAL
{
    public class DatabaseManager
    {
        private string _connectionString;

        public static User AuthenticatedUser { get; private set; }

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

        /// <summary>
        /// Аутентифікація користувача на основі email, username і пароля.
        /// </summary>
        /// <param name="email">Електронна пошта користувача</param>
        /// <param name="username">Ім'я користувача</param>
        /// <param name="password">Пароль користувача</param>
        /// <returns>True, якщо аутентифікація успішна; інакше False</returns>
        public static bool AuthenticateUser(string email, string username, string password)
        {
            using (var context = new CibuuDbContext()) // Створення екземпляра DbContext
            {
                // Знаходимо користувача за email і username
                var user = context.Users.FirstOrDefault(u => u.Email == email && u.Username == username);

                // Якщо користувача не знайдено або пароль неправильний
                if (user == null || !PasswordHasher.Verify(password, user.PasswordHash))
                    return false;

                // Зберігаємо дані авторизованого користувача
                AuthenticatedUser = user;
                return true;
            }
        }

        /// <summary>
        /// Скидає інформацію про авторизованого користувача.
        /// </summary>
        public static void LogoutUser()
        {
            AuthenticatedUser = null;
        }
    }
}

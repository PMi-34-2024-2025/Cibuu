using System.Security.Cryptography;
using System.Text;

namespace Cibuu.DAL.Utilities
{
    public class PasswordHasher // Змінено internal на public
    {
        /// <summary>
        /// Генерує хеш пароля за допомогою SHA-256.
        /// </summary>
        /// <param name="password">Пароль, який потрібно хешувати.</param>
        /// <returns>Хешований пароль у вигляді рядка Base64.</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Пароль не може бути порожнім.", nameof(password));

            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}

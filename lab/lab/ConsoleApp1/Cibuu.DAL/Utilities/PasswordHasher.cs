using BCrypt.Net;
using System;

namespace Cibuu.DAL.Utilities
{
    public class PasswordHasher
    {
        /// <summary>
        /// Генерує хеш пароля за допомогою BCrypt.
        /// </summary>
        /// <param name="password">Пароль, який потрібно хешувати.</param>
        /// <returns>Хешований пароль у вигляді рядка.</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Пароль не може бути порожнім.", nameof(password));

            // Генерація хешу з використанням BCrypt
            string salt = BCrypt.Net.BCrypt.GenerateSalt(); // Генерація солі
            return BCrypt.Net.BCrypt.HashPassword(password, salt); // Хешування пароля з сіллю
        }

        /// <summary>
        /// Перевіряє, чи співпадає введений пароль з збереженим хешом.
        /// </summary>
        /// <param name="password">Пароль, який потрібно перевірити.</param>
        /// <param name="storedHash">Збережений хеш пароля.</param>
        /// <returns>True, якщо пароль правильний; інакше false.</returns>
        public static bool Verify(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
    }
}

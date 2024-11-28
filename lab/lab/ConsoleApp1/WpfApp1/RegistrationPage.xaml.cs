using Cibuu.DAL.models;
using Cibuu.DAL;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Cibuu.DAL.Utilities;

namespace WpfApp1
{
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == "Ім'я користувача" || textBox.Text == "Email")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox.Name == "UsernameTextBox")
                    textBox.Text = "Ім'я користувача";
                else if (textBox.Name == "EmailTextBox")
                    textBox.Text = "Email";

                textBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox.Tag.ToString() == "Пароль")
            {
                passwordBox.Tag = string.Empty;
                passwordBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                passwordBox.Tag = "Пароль";
                passwordBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;

            // Перевірка заповнення
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Усі поля мають бути заповнені.");
                return;
            }

            // Перевірка коректності email
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Неправильний формат email. Перевірте наявність '@' та домену.");
                return;
            }

            try
            {
                using (var context = new CibuuDbContext())
                {
                    // Перевірка наявності користувача з таким же email
                    var existingUser = context.Users.FirstOrDefault(u => u.Email == email);
                    if (existingUser != null)
                    {
                        MessageBox.Show(
                            "Користувач із таким email вже існує. Використовуйте інший email або увійдіть у свій обліковий запис.",
                            "Email вже використовується",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                        return; // Зупиняємо процес реєстрації
                    }

                    // Хешування пароля
                    string passwordHash = PasswordHasher.HashPassword(password);

                    // Додаємо нового користувача
                    var newUser = new User
                    {
                        Username = username,
                        Email = email,
                        PasswordHash = passwordHash,
                        FavoritePlaces = new string[] { } // Пустий список
                    };

                    context.Users.Add(newUser);
                    context.SaveChanges();
                }

                MessageBox.Show("Реєстрація успішна!");

                // Переходимо на іншу сторінку після успішної реєстрації (або залишаємося)
                MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.MainFrame.Navigate(new SearchPage());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка реєстрації: {ex.Message}");
            }
        }

        private bool IsValidEmail(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }
    }
}

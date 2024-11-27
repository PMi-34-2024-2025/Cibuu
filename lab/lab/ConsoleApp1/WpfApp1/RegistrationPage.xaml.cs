using System.Windows;
using System.Windows.Controls;

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
            if (UsernameTextBox.Text == "Ім'я користувача" || string.IsNullOrWhiteSpace(UsernameTextBox.Text))
            {
                MessageBox.Show("Ім'я користувача є обов'язковим.");
                return;
            }

            if (EmailTextBox.Text == "Email" || string.IsNullOrWhiteSpace(EmailTextBox.Text) || !EmailTextBox.Text.Contains("@"))
            {
                MessageBox.Show("Введіть коректний email.");
                return;
            }

            if (string.IsNullOrWhiteSpace(PasswordBox.Password) || PasswordBox.Tag.ToString() == "Пароль")
            {
                MessageBox.Show("Пароль є обов'язковим.");
                return;
            }

            if (PasswordBox.Password.Length < 6)
            {
                MessageBox.Show("Пароль має містити принаймні 6 символів.");
                return;
            }

            MessageBox.Show("Реєстрація успішна!");
        }
    }
}

using System.Windows;
using System.Windows.Controls;
using Cibuu.DAL; 

namespace WpfApp1
{
    public partial class SignInPage : Page
    {
        public SignInPage()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == "Email" || textBox.Text == "Ім'я користувача")
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
                if (textBox.Name == "EmailTextBox")
                    textBox.Text = "Email";
                else if (textBox.Name == "NameTextBox")
                    textBox.Text = "Ім'я користувача";

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

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            string username = NameTextBox.Text.Trim();
            string password = PasswordBox.Password;

            bool isAuthenticated = DatabaseManager.AuthenticateUser(email, username, password);

            if (isAuthenticated)
            {
                MessageBox.Show("Login successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.MainFrame.Navigate(new SearchPage());

                    mainWindow.ShowUserPageButton();
                }
            }
            else
            {
                MessageBox.Show("Invalid email, username, or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}

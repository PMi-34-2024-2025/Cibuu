using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class TopBar : UserControl
    {
        // Подія для обробки кліків по CIBUU
        public event EventHandler CIBUUClicked;

        public TopBar()
        {
            InitializeComponent();
        }

        // Відображення кнопки "User Page" після успішної аутентифікації
        public void ShowUserPageButton()
        {
            UserPageButton.Visibility = Visibility.Visible;
        }

        // Обробка кліку по кнопці CIBUU
        private void CIBUUButton_Click(object sender, RoutedEventArgs e)
        {
            CIBUUClicked?.Invoke(this, EventArgs.Empty);
        }

        // Обробка кліку по кнопці "User Page"
        // Обробник для кнопки UserPage
        private void UserPageButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                try
                {
                    mainWindow.MainFrame.Navigate(new UserPage());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Navigation failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        // Видалення тексту "search" при отриманні фокусу
        private void RemoveText(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Text == "search")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        // Додавання тексту "search", якщо поле порожнє
        private void AddText(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "search";
                textBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        // Перехід до сторінки рекомендацій
        private void OpenRecommendations_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow?.MainFrame.Navigate(new RecommendationsPage());
        }

        // Перехід до сторінки пошуку
        private void OpenSearchPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow?.MainFrame.Navigate(new SearchPage());
        }

        // Перехід до сторінки реєстрації
        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow?.MainFrame.Navigate(new RegistrationPage());
        }

        // Перехід до сторінки входу
        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow?.MainFrame.Navigate(new SignInPage());
        }

        // Перехід до адмін-панелі
        private void OpenAdminPanel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow?.MainFrame.Navigate(new AdminPanel());
        }


    }
}

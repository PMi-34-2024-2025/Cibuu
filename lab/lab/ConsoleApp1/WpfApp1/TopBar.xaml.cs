using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class TopBar : UserControl
    {
        public TopBar()
        {
            InitializeComponent();
        }

        // Метод для видалення тексту з поля пошуку
        private void RemoveText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "search")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }
        }

        // Метод для додавання тексту до поля пошуку, якщо воно пусте
        private void AddText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "search";
                textBox.Foreground = Brushes.Gray;
            }
        }

        // Метод для переходу до сторінки рекомендацій
        private void OpenRecommendations_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new RecommendationsPage());
            }
        }

        // Метод для переходу до сторінки пошуку
        private void OpenSearchPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new SearchPage());
            }
        }

        // Метод для переходу до сторінки реєстрації
        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new RegistrationPage());
            }
        }
        private void OpenAdminPanel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new AdminPanel());
            }
        }


    }
}

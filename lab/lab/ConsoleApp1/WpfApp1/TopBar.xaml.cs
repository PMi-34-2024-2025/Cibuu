using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Cibuu.DAL; // Потрібно підключити для доступу до даних ресторанів

namespace WpfApp1
{
    public partial class TopBar : UserControl
    {
        public event EventHandler? CIBUUClicked;

        public TopBar()
        {
            InitializeComponent();
        }

        // Видалення тексту при фокусі
        private void RemoveText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "пошук")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }
        }

        // Додавання тексту, якщо поле порожнє
        private void AddText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "пошук";
                textBox.Foreground = Brushes.Gray;
            }
        }

        // Оновлений метод пошуку
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchBox = sender as TextBox;
            if (searchBox != null)
            {
                string searchText = searchBox.Text.Trim();

                // Перевірка, чи текст введено та не є значенням для замовчування
                if (!string.IsNullOrWhiteSpace(searchText) && searchText != "пошук")
                {
                    try
                    {
                        using (var context = new CibuuDbContext())
                        {
                            var matchingRestaurants = context.Restaurants
                                .Where(r => r.Name.Contains(searchText)) // Пошук за частиною назви
                                .Select(r => r.Name)
                                .ToList();

                            if (matchingRestaurants.Any())
                            {
                                if (SearchResultsList != null)
                                {
                                    SearchResultsList.ItemsSource = matchingRestaurants;
                                    SearchResultsList.Visibility = Visibility.Visible;  // Показуємо список результатів
                                }
                            }
                            else
                            {
                                if (SearchResultsList != null)
                                {
                                    SearchResultsList.Visibility = Visibility.Collapsed;  // Ховаємо список результатів
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка отримання даних: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    if (SearchResultsList != null)
                    {
                        SearchResultsList.Visibility = Visibility.Collapsed;  // Ховаємо список при порожньому тексті
                    }
                }
            }
        }

        // Обробка кліку на результат пошуку
        private void SearchResult_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock)
            {
                string selectedRestaurant = textBlock.Text;

                // Пошук ресторану в базі даних для отримання його ID
                using (var context = new CibuuDbContext())
                {
                    var restaurant = context.Restaurants
                                            .FirstOrDefault(r => r.Name == selectedRestaurant);

                    if (restaurant != null)
                    {
                        // Передача restaurantId на сторінку ресторану
                        NavigateToRestaurantPage(restaurant.RestaurantId);
                    }
                }

                // Приховуємо список після вибору
                if (SearchResultsList != null)
                {
                    SearchResultsList.Visibility = Visibility.Collapsed;
                }
            }
        }

        // Перехід на сторінку ресторану
        private void NavigateToRestaurantPage(int restaurantId)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                var restaurantPage = new RestaurantPage(restaurantId); // Створення сторінки ресторану з ID
                mainWindow.MainFrame.Navigate(restaurantPage); // Перехід на сторінку ресторану
            }
        }

        // Інші методи для кнопок
        private void OpenRecommendations_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new RecommendationsPage());
            }
        }

        private void OpenSearchPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new SearchPage());
            }
        }

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

        private void CIBUUButton_Click(object sender, RoutedEventArgs e)
        {
            CIBUUClicked?.Invoke(this, EventArgs.Empty);
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new SignInPage());
            }
        }
    }
}

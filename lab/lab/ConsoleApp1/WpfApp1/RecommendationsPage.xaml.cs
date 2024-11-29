using System.Linq;
using System.Windows.Controls;
using Cibuu.DAL;
using Cibuu.DAL.models;
using Cibuu.DAL.Utilities; // Модель ресторану
using Microsoft.EntityFrameworkCore;

namespace WpfApp1
{
    public partial class RecommendationsPage : Page
    {
        public RecommendationsPage()
        {
            InitializeComponent();
            LoadRecommendations(); // Завантажуємо ресторани
        }

        // Завантажуємо ресторан з бази даних за ID
        private void LoadRecommendations()
        {
            using (var context = new CibuuDbContext())
            {
                // Список ресторанів, які ви хочете показати (за їхніми ID)
                var recommendedIds = new[] { 1, 2, 3, 4 }; // Задайте свої ID ресторанців

                var recommendedRestaurants = context.Restaurants
                    .Where(r => recommendedIds.Contains(r.RestaurantId)) // Фільтруємо ресторани
                    .ToList();

                RecommendedList.ItemsSource = recommendedRestaurants; // Виводимо в список
            }
        }

        // Обробник події для вибору ресторану
        private void RecommendedList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecommendedList.SelectedItem is Restaurant selectedRestaurant)
            {
                var frame = (Frame)System.Windows.Application.Current.MainWindow.FindName("MainFrame");
                frame.Navigate(new RestaurantPage(selectedRestaurant.RestaurantId)); // Переходимо на сторінку ресторану
            }
        }
    }
}

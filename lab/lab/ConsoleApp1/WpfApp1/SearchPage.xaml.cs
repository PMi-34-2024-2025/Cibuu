using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media; // Додано для роботи з кольорами
using Cibuu.DAL.models;

namespace WpfApp1
{
    public partial class SearchPage : Page
    {
        private List<Restaurant> _restaurants;

        public SearchPage()
        {
            InitializeComponent();
            LoadRestaurants();
            RestaurantList.ItemsSource = _restaurants;
        }

        private void LoadRestaurants()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant { Name = "The Urban Grill", Description = "Fusion of American and Mediterranean flavors", Location = "Downtown" },
                new Restaurant { Name = "Bella Cucina", Description = "Homemade pasta and pizza", Location = "Central Avenue" },
                new Restaurant { Name = "Sushi Zen", Description = "Fresh fish and creative rolls", Location = "Main Street" },
                new Restaurant { Name = "The Spice House", Description = "Aromatic Indian cuisine", Location = "Old Town" },
                new Restaurant { Name = "Sea Breeze Café", Description = "Seafood-focused café", Location = "Near the beach" },
                new Restaurant { Name = "Steakhouse 56", Description = "Premium steakhouse", Location = "Business District" }
            };
        }

        private void AddToFavorites_Click(object sender, RoutedEventArgs e)
        {
            if (RestaurantList.SelectedItem is Restaurant selectedRestaurant)
            {
                MessageBox.Show($"{selectedRestaurant.Name} додано до улюблених!");
                // Логіка додавання до улюблених (приклад: додати в локальний список улюблених)
            }
        }

        private void ViewDetails_Click(object sender, RoutedEventArgs e)
        {
            if (RestaurantList.SelectedItem is Restaurant selectedRestaurant)
            {
                MessageBox.Show($"Детальна інформація про {selectedRestaurant.Name}:\n\n{selectedRestaurant.Description}\nРозташування: {selectedRestaurant.Location}");
                // Логіка переходу до сторінки деталей
            }
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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedCuisine = null;

            // Отримуємо обраний тип кухні
            StackPanel cuisineStackPanel = (this.FindName("CuisineExpander") as Expander)?.Content as StackPanel;

            if (cuisineStackPanel != null)
            {
                foreach (var child in cuisineStackPanel.Children)
                {
                    if (child is RadioButton radioButton && radioButton.IsChecked == true)
                    {
                        selectedCuisine = radioButton.Content.ToString();
                        break;
                    }
                }
            }

            // Фільтруємо ресторани
            var filteredRestaurants = _restaurants;
            if (!string.IsNullOrEmpty(selectedCuisine))
            {
                filteredRestaurants = _restaurants
                    .Where(r => r.Description.Contains(selectedCuisine, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Оновлюємо список
            RestaurantList.ItemsSource = filteredRestaurants;
        }




    }
}
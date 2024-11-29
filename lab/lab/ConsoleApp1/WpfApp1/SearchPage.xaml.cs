using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media;
using Cibuu.DAL.models;
using Cibuu.DAL;

namespace WpfApp1
{
    public partial class SearchPage : Page
    {
        private List<Restaurant> _restaurants;

        public SearchPage()
        {
            InitializeComponent();
            LoadRestaurants();
        }


        private void LoadRestaurants()
        {
            using (var context = new CibuuDbContext())
            {
                // Завантажуємо всі ресторани з бази даних
                _restaurants = context.Restaurants.ToList();
            }

            // Прив’язуємо дані до списку
            RestaurantList.ItemsSource = _restaurants;
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
            // Отримуємо фільтри
            string selectedCuisine = null;
            bool? isOpen = null;
            bool? isPetFriendly = null;

            // Отримуємо обраний тип кухні
            if (CuisineExpander.Content is StackPanel cuisineStackPanel)
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

            // Фільтр "Відкрито/Закрито"
            if (OpenExpander.Content is StackPanel openStackPanel)
            {
                foreach (var child in openStackPanel.Children)
                {
                    if (child is RadioButton radioButton && radioButton.IsChecked == true)
                    {
                        isOpen = radioButton.Content.ToString() == "Open";
                        break;
                    }
                }
            }

            // Фільтр "Pet-Friendly"
            if (PetFriendlyExpander.Content is StackPanel petFriendlyStackPanel)
            {
                foreach (var child in petFriendlyStackPanel.Children)
                {
                    if (child is RadioButton radioButton && radioButton.IsChecked == true)
                    {
                        isPetFriendly = radioButton.Content.ToString() == "Yes";
                        break;
                    }
                }
            }

            // Фільтруємо дані
            using (var context = new CibuuDbContext())
            {
                var filteredRestaurants = context.Restaurants.AsQueryable();

                if (!string.IsNullOrEmpty(selectedCuisine))
                    filteredRestaurants = filteredRestaurants.Where(r => r.Cuisine.Contains(selectedCuisine));

                if (isOpen.HasValue)
                    filteredRestaurants = filteredRestaurants.Where(r => r.IsOpen == isOpen.Value);

                if (isPetFriendly.HasValue)
                    filteredRestaurants = filteredRestaurants.Where(r => r.PetFriendly == isPetFriendly.Value);

                // Оновлюємо список
                RestaurantList.ItemsSource = filteredRestaurants.ToList();
            }
            UpdateSelectedFilters();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Скидаємо фільтри
            if (CuisineExpander.Content is StackPanel cuisineStackPanel)
            {
                foreach (var child in cuisineStackPanel.Children)
                {
                    if (child is RadioButton radioButton)
                    {
                        radioButton.IsChecked = false;
                    }
                }
            }

            if (OpenExpander.Content is StackPanel openStackPanel)
            {
                foreach (var child in openStackPanel.Children)
                {
                    if (child is RadioButton radioButton)
                    {
                        radioButton.IsChecked = false;
                    }
                }
            }

            if (PetFriendlyExpander.Content is StackPanel petFriendlyStackPanel)
            {
                foreach (var child in petFriendlyStackPanel.Children)
                {
                    if (child is RadioButton radioButton)
                    {
                        radioButton.IsChecked = false;
                    }
                }
            }

            // Відновлюємо повний список ресторанів
            LoadRestaurants();
            UpdateSelectedFilters();
        }

        private void UpdateSelectedFilters()
        {
            // Очищуємо попередні фільтри
            SelectedFiltersTextBlock.Children.Clear();

            // Отримуємо обраний тип кухні
            if (CuisineExpander.Content is StackPanel cuisineStackPanel)
            {
                foreach (var child in cuisineStackPanel.Children)
                {
                    if (child is RadioButton radioButton && radioButton.IsChecked == true)
                    {
                        AddFilterToWrapPanel(radioButton.Content.ToString());
                    }
                }
            }

            // Фільтр "Відкрито/Закрито"
            if (OpenExpander.Content is StackPanel openStackPanel)
            {
                foreach (var child in openStackPanel.Children)
                {
                    if (child is RadioButton radioButton && radioButton.IsChecked == true)
                    {
                        AddFilterToWrapPanel(radioButton.Content.ToString());
                    }
                }
            }

            // Фільтр "Pet-Friendly"
            if (PetFriendlyExpander.Content is StackPanel petFriendlyStackPanel)
            {
                foreach (var child in petFriendlyStackPanel.Children)
                {
                    if (child is RadioButton radioButton && radioButton.IsChecked == true)
                    {
                        AddFilterToWrapPanel(radioButton.Content.ToString());
                    }
                }
            }
        }

        // Додає текстовий фільтр до WrapPanel
        private void AddFilterToWrapPanel(string filterText)
        {
            var textBlock = new TextBlock
            {
                Text = filterText,
                Foreground = Brushes.Blue,
                Margin = new Thickness(0, 0, 10, 0),
                Cursor = System.Windows.Input.Cursors.Hand
            };
            SelectedFiltersTextBlock.Children.Add(textBlock);
        }
        private void SortByNameAscending_Click(object sender, RoutedEventArgs e)
        {
            if (_restaurants != null)
            {
                RestaurantList.ItemsSource = _restaurants.OrderBy(r => r.Name).ToList();
            }
        }

        private void SortByNameDescending_Click(object sender, RoutedEventArgs e)
        {
            if (_restaurants != null)
            {
                RestaurantList.ItemsSource = _restaurants.OrderByDescending(r => r.Name).ToList();
            }
        }

        private void RestaurantList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RestaurantList.SelectedItem is Restaurant selectedRestaurant)
            {
                // Переходимо на сторінку ресторану
                var mainWindow = Window.GetWindow(this) as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.MainFrame.Navigate(new RestaurantPage(selectedRestaurant.RestaurantId));
                }
            }
        }


    }
}


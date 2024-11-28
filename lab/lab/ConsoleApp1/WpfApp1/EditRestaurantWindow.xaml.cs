using System.Windows;
using Cibuu.DAL.models;

namespace WpfApp1
{
    public partial class EditRestaurantWindow : Window
    {
        public Restaurant Restaurant { get; set; }

        public EditRestaurantWindow(Restaurant restaurant)
        {
            InitializeComponent();
            Restaurant = restaurant;

            // Ініціалізуємо поля
            NameTextBox.Text = Restaurant.Name;
            DescriptionTextBox.Text = Restaurant.Description;
            CuisineTextBox.Text = Restaurant.Cuisine;
            LocationTextBox.Text = Restaurant.Location;
            RatingTextBox.Text = Restaurant.Rating.ToString();
            IsOpenCheckBox.IsChecked = Restaurant.IsOpen;
            PetFriendlyCheckBox.IsChecked = Restaurant.PetFriendly;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Зберігаємо значення
            Restaurant.Name = NameTextBox.Text;
            Restaurant.Description = DescriptionTextBox.Text;
            Restaurant.Cuisine = CuisineTextBox.Text;
            Restaurant.Location = LocationTextBox.Text;

            if (double.TryParse(RatingTextBox.Text, out double rating))
            {
                Restaurant.Rating = rating;
            }
            else
            {
                MessageBox.Show("Некоректне значення для рейтингу.");
                return;
            }

            Restaurant.IsOpen = IsOpenCheckBox.IsChecked == true;
            Restaurant.PetFriendly = PetFriendlyCheckBox.IsChecked == true;

            DialogResult = true; // Закриваємо вікно
        }
    }
}

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

            // Заповнюємо поля даними закладу
            NameTextBox.Text = Restaurant.Name;
            DescriptionTextBox.Text = Restaurant.Description;
            CuisineTextBox.Text = Restaurant.Cuisine;
            LocationTextBox.Text = Restaurant.Location;
            RatingTextBox.Text = Restaurant.Rating.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Оновлюємо дані закладу
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

            DialogResult = true; // Закриваємо вікно з успішним результатом
        }
    }
}

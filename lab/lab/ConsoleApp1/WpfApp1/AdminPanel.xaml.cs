using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Cibuu.DAL;
using Cibuu.DAL.models;

namespace WpfApp1
{
    public partial class AdminPanel : Page
    {
        private CibuuDbContext _context;

        public AdminPanel()
        {
            InitializeComponent();
            _context = new CibuuDbContext();
            LoadRestaurants();
        }

        private void LoadRestaurants()
        {
            RestaurantDataGrid.ItemsSource = _context.Restaurants.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var newRestaurant = new Restaurant(); // Порожній об'єкт для заповнення
            var addWindow = new EditRestaurantWindow(newRestaurant);
            if (addWindow.ShowDialog() == true)
            {
                _context.Restaurants.Add(newRestaurant); // Додаємо до бази
                _context.SaveChanges();
                LoadRestaurants(); // Оновлюємо список
                MessageBox.Show("Заклад додано!");
            }
        }


        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (RestaurantDataGrid.SelectedItem is Restaurant selectedRestaurant)
            {
                var editWindow = new EditRestaurantWindow(selectedRestaurant);
                if (editWindow.ShowDialog() == true)
                {
                    // Зберігаємо зміни в базі
                    _context.Restaurants.Update(selectedRestaurant);
                    _context.SaveChanges();
                    LoadRestaurants();
                    MessageBox.Show("Заклад оновлено!");
                }
            }
            else
            {
                MessageBox.Show("Оберіть заклад для редагування.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (RestaurantDataGrid.SelectedItem is Restaurant selectedRestaurant)
            {
                _context.Restaurants.Remove(selectedRestaurant);
                _context.SaveChanges();
                LoadRestaurants();
                MessageBox.Show("Заклад видалено!");
            }
            else
            {
                MessageBox.Show("Оберіть заклад для видалення.");
            }
        }
    }
}

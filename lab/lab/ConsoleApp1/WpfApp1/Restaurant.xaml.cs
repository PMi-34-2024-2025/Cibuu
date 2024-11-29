using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Cibuu.DAL;
using Cibuu.DAL.models;  // Модель для ресторану
using Microsoft.EntityFrameworkCore;

namespace WpfApp1
{
    public partial class RestaurantPage : Page
    {
        private int _restaurantId;

        public RestaurantPage(int restaurantId)
        {
            InitializeComponent();
            _restaurantId = restaurantId;
            LoadRestaurantData();
        }

        // Завантажуємо дані ресторану з бази даних
        private void LoadRestaurantData()
        {
            using (var context = new CibuuDbContext())
            {
                var restaurant = context.Restaurants
                    .FirstOrDefault(r => r.RestaurantId == _restaurantId);

                if (restaurant != null)
                {
                    RestaurantName.Text = restaurant.Name;
                    RestaurantAddress.Text = restaurant.Location;
                    RestaurantCuisine.Text = $"Cuisine: {restaurant.Cuisine}";
                    RestaurantDescription.Text = restaurant.Description;
                    RestaurantRating.Text = $"Rating: {restaurant.Rating}";
                }
            }
        }

        // Обробник для лайку ресторану
        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            // Логіка для лайку (статична)
            MessageBox.Show("You liked this restaurant! ❤️");

            // Показуємо повідомлення про додавання в улюблені
            FavoriteMessage.Visibility = Visibility.Visible;

            // Відключаємо повідомлення після 2 секунд
            var timer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = System.TimeSpan.FromSeconds(2)
            };
            timer.Tick += (s, args) =>
            {
                FavoriteMessage.Visibility = Visibility.Collapsed;
                timer.Stop();
            };
            timer.Start();
        }

        // Обробник для відправки відгуку
        private void SubmitReviewButton_Click(object sender, RoutedEventArgs e)
        {
            // Якщо користувач не написав відгук
            if (string.IsNullOrWhiteSpace(ReviewTextBox.Text))
            {
                MessageBox.Show("Please write a review!");
                return;
            }

            // Статичне додавання відгуку
            string review = ReviewTextBox.Text;

            // Оновлення UI зі статичним відгуком
            var newReviewBorder = new Border
            {
                BorderBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Gray),
                BorderThickness = new System.Windows.Thickness(1),
                Padding = new System.Windows.Thickness(10),
                Margin = new System.Windows.Thickness(0, 0, 0, 10),
                Child = new StackPanel
                {
                    Children =
                    {
                        new TextBlock { Text = review, FontWeight = FontWeights.Bold },
                        new TextBlock { Text = "Rating: 5 stars" }
                    }
                }
            };

           // ReviewList.Children.Add(newReviewBorder);
            ReviewTextBox.Clear();  // Очищаємо поле після відправки
        }
    }
}

using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Cibuu.DAL;
using Cibuu.DAL.models;

namespace WpfApp1
{
    public partial class RestaurantPage : Page
    {
        private int _restaurantId;
        private int _currentUserId = 1; // Замініть на реальну авторизацію, щоб отримати ID користувача

        public RestaurantPage(int restaurantId)
        {
            InitializeComponent();
            _restaurantId = restaurantId;
            LoadRestaurantData();
            LoadReviews();
        }

        // Завантаження даних ресторану
        private void LoadRestaurantData()
        {
            using (var context = new CibuuDbContext())
            {
                var restaurant = context.Restaurants.FirstOrDefault(r => r.RestaurantId == _restaurantId);

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

        // Завантаження відгуків
        private void LoadReviews()
        {
            using (var context = new CibuuDbContext())
            {
                var reviews = context.Reviews
                    .Where(r => r.RestaurantId == _restaurantId)
                    .OrderByDescending(r => r.ReviewDate)
                    .ToList();

                ReviewsContainer.ItemsSource = reviews; // Прив'язка до списку відгуків
            }
        }


        // Збереження нового відгуку
        private void SubmitReviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentUserId == 0)
            {
                MessageBox.Show("Тільки зареєстровані користувачі можуть залишати коментарі!");
                return;
            }

            if (string.IsNullOrWhiteSpace(ReviewTextBox.Text))
            {
                MessageBox.Show("Поле коментаря не може бути порожнім!");
                return;
            }

            using (var context = new CibuuDbContext())
            {
                var review = new Review
                {
                    UserId = _currentUserId, // Замінити на реального користувача
                    RestaurantId = _restaurantId,
                    Text = ReviewTextBox.Text, // Це поле відповідає стовпцю "text"
                    Rate = 5, // Можна додати механізм вибору рейтингу
                    ReviewDate = DateTime.UtcNow // Зберігаємо час у форматі UTC
                };

                context.Reviews.Add(review);
                context.SaveChanges();
            }

            ReviewTextBox.Clear();
            LoadReviews(); // Перезавантажуємо список відгуків
            MessageBox.Show("Відгук успішно додано!");
        }



        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You liked this restaurant! ❤️");
        }

    }
}

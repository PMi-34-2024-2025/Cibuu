using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Cibuu.DAL;
using Cibuu.DAL.models;

namespace WpfApp1
{
    public partial class RestaurantPage : Page
    {
        private int _restaurantId;
        private int _currentUserId = 1;

        public RestaurantPage(int restaurantId)
        {
            InitializeComponent();
            _restaurantId = restaurantId;
            LoadRestaurantData();
            LoadReviews();
        }

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

        private void LoadReviews()
        {
            using (var context = new CibuuDbContext())
            {
                var reviews = context.Reviews
                    .Where(r => r.RestaurantId == _restaurantId)
                    .OrderByDescending(r => r.ReviewDate)
                    .ToList();

                ReviewsContainer.ItemsSource = reviews;
            }
        }

        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            var animation = (Storyboard)FindResource("HeartAnimation");
            animation.Begin();
            FavoriteMessage.Visibility = Visibility.Visible;

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
                var newReview = new Review
                {
                    UserId = _currentUserId,
                    RestaurantId = _restaurantId,
                    Text = ReviewTextBox.Text,
                    // Перетворення часу на UTC
                    ReviewDate = DateTime.UtcNow
                };

                context.Reviews.Add(newReview);
                context.SaveChanges();
            }

            // Очищаємо поле для коментаря
            ReviewTextBox.Clear();

            // Перезавантажуємо список відгуків
            LoadReviews();

            MessageBox.Show("Відгук успішно додано!");
        }

    }
}

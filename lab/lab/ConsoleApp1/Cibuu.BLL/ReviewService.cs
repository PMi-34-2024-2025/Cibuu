using Cibuu.DAL.models;
using Cibuu.DAL;
using Npgsql;

public class ReviewService
{
    private DatabaseManager _dbManager;

    public ReviewService(DatabaseManager dbManager)
    {
        _dbManager = dbManager;
    }

    public List<Review> GetAllReviews()
    {
        List<Review> reviews = new List<Review>();
        string query = "SELECT * FROM Reviews";

        using (var connection = _dbManager.GetConnection())
        {
            using (var command = new NpgsqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reviews.Add(new Review
                        {
                            ReviewId = (int)reader["review_id"],
                            UserId = (int)reader["user_id"],
                            RestaurantId = (int)reader["restaurant_id"],
                            Rate = (int)reader["rate"],
                            Text = (string)reader["text"],
                            ReviewDate = (DateTime)reader["review_date"]
                        });
                    }
                }
            }
        }

        return reviews;
    }

    public void InsertRandomReviews()
    {
        Random random = new Random();

        using (var connection = _dbManager.GetConnection())
        {
            // Отримуємо всі наявні користувачі та ресторани
            List<int> userIds = new List<int>();
            List<int> restaurantIds = new List<int>();

            // Отримуємо список існуючих користувачів
            string userQuery = "SELECT user_id FROM Users";
            using (var userCommand = new NpgsqlCommand(userQuery, connection))
            {
                using (var reader = userCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userIds.Add((int)reader["user_id"]);
                    }
                }
            }

            // Отримуємо список існуючих ресторанів
            string restaurantQuery = "SELECT restaurant_id FROM Restaurants";
            using (var restaurantCommand = new NpgsqlCommand(restaurantQuery, connection))
            {
                using (var reader = restaurantCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        restaurantIds.Add((int)reader["restaurant_id"]);
                    }
                }
            }

            // Перевіряємо, чи є хоча б один користувач і ресторан
            if (userIds.Count == 0 || restaurantIds.Count == 0)
            {
                Console.WriteLine("Не можна додати відгуки: немає користувачів або ресторанів.");
                return;
            }

            // Вставляємо випадкові відгуки
            for (int i = 0; i < 10; i++)
            {
                int userId = userIds[random.Next(userIds.Count)];
                int restaurantId = restaurantIds[random.Next(restaurantIds.Count)];
                int rate = random.Next(1, 6);  // Оцінка від 1 до 5

                string query = $"INSERT INTO Reviews (user_id, restaurant_id, rate, text) VALUES ({userId}, {restaurantId}, {rate}, 'Great place!')";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Таблиця Reviews заповнена випадковими даними.");
        }
    }

}

using Cibuu.DAL;
using Cibuu.DAL.models;
using Npgsql;
using System.Collections.Generic;

namespace Cibuu.BLL
{
    public class RestaurantService
    {
        private DatabaseManager _dbManager;

        public RestaurantService(DatabaseManager dbManager)
        {
            _dbManager = dbManager;
        }

        public List<Restaurant> GetAllRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            string query = "SELECT * FROM Restaurants";

            using (var connection = _dbManager.GetConnection())
            {
                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            restaurants.Add(new Restaurant
                            {
                                RestaurantId = (int)reader["restaurant_id"],
                                Name = (string)reader["name"],
                                Description = (string)reader["description"],
                                Reviews = (string[])reader["reviews"],
                                Location = (string)reader["location"]
                            });
                        }
                    }
                }
            }

            return restaurants;
        }

        public void InsertRandomRestaurants()
        {
            using (var connection = _dbManager.GetConnection())
            {
                // Перевіряємо, чи є записи в таблиці
                string checkQuery = "SELECT COUNT(*) FROM Restaurants";
                using (var checkCommand = new NpgsqlCommand(checkQuery, connection))
                {
                    long count = (long)checkCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        Console.WriteLine("Таблиця Restaurants вже містить дані.");
                        return; // Якщо дані є, виходимо з методу
                    }
                }

                // Якщо таблиця порожня, заповнюємо її випадковими даними
                string[] names = { "Restaurant A", "Restaurant B", "Restaurant C" };
                string[] locations = { "Location A", "Location B", "Location C" };
                Random random = new Random();

                for (int i = 0; i < 10; i++)
                {
                    string name = names[random.Next(names.Length)];
                    string location = locations[random.Next(locations.Length)];
                    string query = $"INSERT INTO Restaurants (name, description, reviews, location) VALUES ('{name}', 'Good restaurant', '{{\"Review1\",\"Review2\"}}', '{location}')";
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Таблиця Restaurants заповнена випадковими даними.");
            }
        }

    }
}

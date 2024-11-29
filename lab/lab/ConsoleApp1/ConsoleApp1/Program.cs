using System;
using Npgsql;  
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
<<<<<<< HEAD
        // Рядок підключення до PostgreSQL (заміни дані на свої)
        string connectionString = "Host=localhost;Username=postgres;Password=23092005;Database=cibuu";
=======
        string connectionString = "Host=localhost;Username=postgres;Password=Vinzer1979;Database=cibuu";
>>>>>>> 70731f2d8e75b5ef494e5419e97b2629898b599b

        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine("Підключення до бази даних успішно встановлено.");

            InsertRandomUsers(connection);
            InsertRandomRestaurants(connection);
            InsertRandomReviews(connection);
            InsertRandomAdmins(connection);

            PrintUsers(connection);
            PrintRestaurants(connection);
            PrintReviews(connection);
            PrintAdmins(connection);
        }
    }

    static void PrintUsers(NpgsqlConnection connection)
    {
        string query = "SELECT * FROM Users";
        using (var command = new NpgsqlCommand(query, connection))
        {
            using (var reader = command.ExecuteReader())
            {
                Console.WriteLine("Users:");
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["user_id"]}, Name: {reader["username"]}, Email: {reader["email"]}");
                }
            }
        }
    }

    static void PrintRestaurants(NpgsqlConnection connection)
    {
        string query = "SELECT * FROM Restaurants";
        using (var command = new NpgsqlCommand(query, connection))
        {
            using (var reader = command.ExecuteReader())
            {
                Console.WriteLine("Restaurants:");
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["restaurant_id"]}, Name: {reader["name"]}, Location: {reader["location"]}");
                }
            }
        }
    }

    static void PrintReviews(NpgsqlConnection connection)
    {
        string query = "SELECT * FROM Reviews";
        using (var command = new NpgsqlCommand(query, connection))
        {
            using (var reader = command.ExecuteReader())
            {
                Console.WriteLine("Reviews:");
                while (reader.Read())
                {
                    Console.WriteLine($"Review ID: {reader["review_id"]}, Rate: {reader["rate"]}, Review Date: {reader["review_date"]}");
                }
            }
        }
    }

    static void PrintAdmins(NpgsqlConnection connection)
    {
        string query = "SELECT * FROM Admins";
        using (var command = new NpgsqlCommand(query, connection))
        {
            using (var reader = command.ExecuteReader())
            {
                Console.WriteLine("Admins:");
                while (reader.Read())
                {
                    Console.WriteLine($"Admin ID: {reader["admin_id"]}, Username: {reader["username"]}, Role: {reader["role"]}");
                }
            }
        }
    }

    static void InsertRandomUsers(NpgsqlConnection connection)
    {
        string[] names = { "Alice", "Bob", "Charlie", "David", "Eva" };
        string[] emails = { "alice@example.com", "bob@example.com", "charlie@example.com", "david@example.com", "eva@example.com" };
        Random random = new Random();

        for (int i = 0; i < 50; i++)
        {
            string name = names[random.Next(names.Length)];
            string email = emails[random.Next(emails.Length)];
            string query = $"INSERT INTO Users (username, favorite_places, email) VALUES ('{name}', '{{Place1,Place2}}', '{email}')";
            using (var command = new NpgsqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
        Console.WriteLine("Таблиця Users заповнена випадковими даними.");
    }

    static void InsertRandomRestaurants(NpgsqlConnection connection)
    {
        string[] names = { "Restaurant A", "Restaurant B", "Restaurant C" };
        string[] locations = { "Location A", "Location B", "Location C" };
        Random random = new Random();

        for (int i = 0; i < 30; i++)
        {
            string name = names[random.Next(names.Length)];
            string location = locations[random.Next(locations.Length)];
            string query = $"INSERT INTO Restaurants (name, description, reviews, location) VALUES ('{name}', 'Good restaurant', '{{Review1,Review2}}', '{location}')";
            using (var command = new NpgsqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
        Console.WriteLine("Таблиця Restaurants заповнена випадковими даними.");
    }

    static void InsertRandomReviews(NpgsqlConnection connection)
    {
        Random random = new Random();

        for (int i = 0; i < 50; i++)
        {
            int userId = random.Next(1, 51);  
            int restaurantId = random.Next(1, 31); 
            int rate = random.Next(1, 6);  
            string query = $"INSERT INTO Reviews (user_id, restaurant_id, rate, text) VALUES ({userId}, {restaurantId}, {rate}, 'Great place!')";
            using (var command = new NpgsqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
        Console.WriteLine("Таблиця Reviews заповнена випадковими даними.");
    }

    static void InsertRandomAdmins(NpgsqlConnection connection)
    {
        string[] roles = { "Manager", "Supervisor", "Admin" };
        string[] names = { "Admin1", "Admin2", "Admin3" };
        Random random = new Random();

        for (int i = 0; i < 10; i++)
        {
            string role = roles[random.Next(roles.Length)];
            string name = names[random.Next(names.Length)];
            string query = $"INSERT INTO Admins (username, role) VALUES ('{name}', '{role}')";
            using (var command = new NpgsqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
        Console.WriteLine("Таблиця Admins заповнена випадковими даними.");
    }
}

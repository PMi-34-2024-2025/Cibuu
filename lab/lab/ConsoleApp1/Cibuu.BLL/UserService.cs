using Cibuu.DAL.models;
using Cibuu.DAL;
using Npgsql;

public class UserService
{
    private DatabaseManager _dbManager;

    public UserService(DatabaseManager dbManager)
    {
        _dbManager = dbManager;
    }

    public List<User> GetAllUsers()
    {
        List<User> users = new List<User>();
        string query = "SELECT * FROM Users";

        using (var connection = _dbManager.GetConnection())
        {
            using (var command = new NpgsqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            UserId = (int)reader["user_id"],
                            Username = (string)reader["username"],
                            FavoritePlaces = (string[])reader["favorite_places"],
                            Email = (string)reader["email"]
                        });
                    }
                }
            }
        }

        return users;
    }

    public void InsertRandomUsers()
    {
        using (var connection = _dbManager.GetConnection())
        {
            string checkQuery = "SELECT COUNT(*) FROM Users";
            using (var checkCommand = new NpgsqlCommand(checkQuery, connection))
            {
                long count = (long)checkCommand.ExecuteScalar();
                if (count > 0)
                {
                    Console.WriteLine("Таблиця Users вже містить дані.");
                    return;
                }
            }

            string[] names = { "Alice", "Bob", "Charlie", "David", "Eva" };
            string[] emails = { "alice@example.com", "bob@example.com", "charlie@example.com", "david@example.com", "eva@example.com" };
            Random random = new Random();

            for (int i = 0; i < 10; i++)
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
    }
}

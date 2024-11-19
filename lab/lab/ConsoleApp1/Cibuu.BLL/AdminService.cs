using Cibuu.DAL.models;
using Cibuu.DAL;
using Npgsql;

public class AdminService
{
    private DatabaseManager _dbManager;

    public AdminService(DatabaseManager dbManager)
    {
        _dbManager = dbManager;
    }

    public List<Admin> GetAllAdmins()
    {
        List<Admin> admins = new List<Admin>();
        string query = "SELECT * FROM Admins";

        using (var connection = _dbManager.GetConnection())
        {
            using (var command = new NpgsqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        admins.Add(new Admin
                        {
                            AdminId = (int)reader["admin_id"],
                            Role = (string)reader["role"],
                            Username = (string)reader["username"]
                        });
                    }
                }
            }
        }

        return admins;
    }

    public void InsertRandomAdmins()
    {
        using (var connection = _dbManager.GetConnection())
        {
            string checkQuery = "SELECT COUNT(*) FROM Admins";
            using (var checkCommand = new NpgsqlCommand(checkQuery, connection))
            {
                long count = (long)checkCommand.ExecuteScalar();
                if (count > 0)
                {
                    Console.WriteLine("Таблиця Admins вже містить дані.");
                    return;
                }
            }

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
}

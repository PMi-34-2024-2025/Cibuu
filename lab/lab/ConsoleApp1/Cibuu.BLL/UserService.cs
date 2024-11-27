using Cibuu.DAL.models;
using Cibuu.DAL;
using System.Collections.Generic;
using System.Linq;

public class UserService
{
    private readonly CibuuDbContext _context;

    public UserService(CibuuDbContext context)
    {
        _context = context;
    }

    public List<User> GetAllUsers()
    {
        return _context.Users.ToList();
    }

    public void InsertRandomUsers()
    {
        if (_context.Users.Any())
        {
            Console.WriteLine("Таблиця Users вже містить дані.");
            return;
        }

        string[] names = { "Alice", "Bob", "Charlie", "David", "Eva" };
        string[] emails = { "alice@example.com", "bob@example.com", "charlie@example.com", "david@example.com", "eva@example.com" };
        var random = new Random();

        var users = new List<User>();

        for (int i = 0; i < 10; i++)
        {
            users.Add(new User
            {
                Username = names[random.Next(names.Length)],
                Email = emails[random.Next(emails.Length)],
                FavoritePlaces = new[] { "Place1", "Place2" }
            });
        }

        _context.Users.AddRange(users);
        _context.SaveChanges();

        Console.WriteLine("Таблиця Users заповнена випадковими даними.");
    }
}

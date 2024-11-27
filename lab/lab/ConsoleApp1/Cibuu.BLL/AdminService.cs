using Cibuu.DAL;
using Cibuu.DAL.models;
using System.Collections.Generic;
using System.Linq;

public class AdminService
{
    private readonly CibuuDbContext _context;

    public AdminService(CibuuDbContext context)
    {
        _context = context;
    }

    public List<Admin> GetAllAdmins()
    {
        return _context.Admins.ToList();
    }

    public void InsertRandomAdmins()
    {
        if (_context.Admins.Any())
        {
            Console.WriteLine("Таблиця Admins вже містить дані.");
            return;
        }

        string[] roles = { "Manager", "Supervisor", "Admin" };
        string[] names = { "Admin1", "Admin2", "Admin3" };
        var random = new Random();

        var admins = new List<Admin>();

        for (int i = 0; i < 10; i++)
        {
            admins.Add(new Admin
            {
                Role = roles[random.Next(roles.Length)],
                Username = names[random.Next(names.Length)]
            });
        }

        _context.Admins.AddRange(admins);
        _context.SaveChanges();

        Console.WriteLine("Таблиця Admins заповнена випадковими даними.");
    }
}

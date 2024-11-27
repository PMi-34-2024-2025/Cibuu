using Cibuu.DAL;
using Cibuu.DAL.models;
using System.Collections.Generic;
using System.Linq;

public class RestaurantService
{
    private readonly CibuuDbContext _context;

    public RestaurantService(CibuuDbContext context)
    {
        _context = context;
    }

    public List<Restaurant> GetAllRestaurants()
    {
        return _context.Restaurants.ToList();
    }

    public void InsertRandomRestaurants()
    {
        if (_context.Restaurants.Any())
        {
            Console.WriteLine("Таблиця Restaurants вже містить дані.");
            return;
        }

        string[] names = { "Restaurant A", "Restaurant B", "Restaurant C" };
        string[] locations = { "Location A", "Location B", "Location C" };
        var random = new Random();

        var restaurants = new List<Restaurant>();

        for (int i = 0; i < 10; i++)
        {
            restaurants.Add(new Restaurant
            {
                Name = names[random.Next(names.Length)],
                Description = "Good restaurant",
                Reviews = new[] { "Review1", "Review2" },
                Location = locations[random.Next(locations.Length)]
            });
        }

        _context.Restaurants.AddRange(restaurants);
        _context.SaveChanges();

        Console.WriteLine("Таблиця Restaurants заповнена випадковими даними.");
    }
}

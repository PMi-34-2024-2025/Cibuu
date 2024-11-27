using Cibuu.DAL;
using Cibuu.DAL.models;
using System.Collections.Generic;
using System.Linq;

public class ReviewService
{
    private readonly CibuuDbContext _context;

    public ReviewService(CibuuDbContext context)
    {
        _context = context;
    }

    public List<Review> GetAllReviews()
    {
        return _context.Reviews.ToList();
    }

    public void InsertRandomReviews()
    {
        if (!_context.Users.Any() || !_context.Restaurants.Any())
        {
            Console.WriteLine("Не можна додати відгуки: немає користувачів або ресторанів.");
            return;
        }

        var random = new Random();
        var reviews = new List<Review>();

        var userIds = _context.Users.Select(u => u.UserId).ToList();
        var restaurantIds = _context.Restaurants.Select(r => r.RestaurantId).ToList();

        for (int i = 0; i < 10; i++)
        {
            reviews.Add(new Review
            {
                UserId = userIds[random.Next(userIds.Count)],
                RestaurantId = restaurantIds[random.Next(restaurantIds.Count)],
                Rate = random.Next(1, 6),
                Text = "Great place!",
                ReviewDate = DateTime.Now
            });
        }

        _context.Reviews.AddRange(reviews);
        _context.SaveChanges();

        Console.WriteLine("Таблиця Reviews заповнена випадковими даними.");
    }
}

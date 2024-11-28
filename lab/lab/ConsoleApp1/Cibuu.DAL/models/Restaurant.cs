public class Restaurant
{
    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string[] Reviews { get; set; } = Array.Empty<string>(); // Встановлено значення за замовчуванням
    public string Location { get; set; }
    public string Cuisine { get; set; }
    public double Distance { get; set; }
    public bool IsOpen { get; set; }
    public bool PetFriendly { get; set; }
    public double Rating { get; set; }
}

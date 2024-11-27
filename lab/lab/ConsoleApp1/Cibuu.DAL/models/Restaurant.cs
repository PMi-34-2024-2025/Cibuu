namespace Cibuu.DAL.models
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] Reviews { get; set; }
        public string Location { get; set; }
        public string Cuisine { get; set; }  // Додано властивість Cuisine

    }
}

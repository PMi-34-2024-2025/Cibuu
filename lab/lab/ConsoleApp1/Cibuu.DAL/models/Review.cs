namespace Cibuu.DAL.models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public int Rate { get; set; }
        public string Text { get; set; }
        public DateTime ReviewDate { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
    }
}

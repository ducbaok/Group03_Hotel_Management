namespace HotelReservation
{
    public class ReviewGroup
    {
        public List<Review> Reviews { get; set; } = new();

        public Rating GetAverageRating()
        {
            return 0;
        }
    }

    public class Review
    {
        public UID UserID { get; set; } = new();
        public Rating Rating { get; set; } = new();
        public string Comment { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}

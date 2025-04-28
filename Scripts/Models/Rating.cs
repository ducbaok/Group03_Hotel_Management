namespace HotelReservation
{
    public struct Rating
    {
        private float _rating;

        public static implicit operator float(Rating rating) => rating._rating;
        public static implicit operator Rating(float rating) => new() { _rating = rating };
    }
}

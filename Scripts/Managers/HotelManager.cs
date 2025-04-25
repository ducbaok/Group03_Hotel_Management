namespace HotelReservation
{
    public class HotelManager
    {
        public Dictionary<UID, Hotel> Hotels = new();

        public bool AddHotel()
        {
            return true;
        }

        public bool DeleteHotel()
        {
            return true;
        }

        public List<UID> SearchHotel(HotelSearchingType type, string query)
        {
            return null;
        }

        public List<UID> FilterHotel(HotelFilteringType type, string query)
        {
            return null;
        }
    }
}

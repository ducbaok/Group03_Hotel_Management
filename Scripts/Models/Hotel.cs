namespace HotelReservation
{
    public class Hotel
    {
        public UID ID { get; set; }
        public HotelDescription Description { get; set; } = new();
        public List<RoomGroup> RoomGroups { get; set; } = new();
    }

    public class HotelDescription
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

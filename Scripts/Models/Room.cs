namespace HotelReservation
{
    public class RoomGroup
    {
        public string Name { get; set; } = string.Empty;
        public RoomDescription Description { get; set; } = new();
        public List<ushort> Numbers { get; set; } = new();
    }
}

namespace HotelReservation
{
    public class Room
    {
        public string Name { get; set; } = string.Empty;
        public RoomDescription Description { get; set; } = new();
        public Dictionary<ushort, RoomStatus> Rooms { get; set; } = new();
    }

    public class RoomStatus
    {
        public bool IsAvailable { get; set; } = true;
    }
}

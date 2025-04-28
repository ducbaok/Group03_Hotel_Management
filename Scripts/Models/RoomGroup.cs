namespace HotelReservation
{
    public class RoomCategorization
    {
        public Dictionary<UID, RoomGroup> Rooms { get; set; } = new(); // Key is ID of RoomGroup
    }

    public class RoomGroup
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

namespace HotelReservation
{
    public class RoomDescription
    {
        public RoomProperty Property { get; set; } = new();
        public RoomAmenity Amenity { get; set; } = new();
        public RoomRestriction Restriction { get; set; } = new();
        public RoomAccessibility Accessibility { get; set; } = new();
    }

    public class RoomProperty
    {
        public ushort MaxOccupancy { get; set; } = 5;
        public ushort NumberOfBeds { get; set; } = 2;
        public BedType BedType { get; set; } = new();
        public ViewType ViewType { get; set; } = ViewType.Ocean;
    }

    public class RoomAmenity
    {
        public bool HasWifi { get; set; } = true;
        public bool HasTV { get; set; } = true;
        public bool HasBalcony { get; set; } = true;
        public bool HasKitchen { get; set; } = false;
        public bool HasBathTub { get; set; } = true;
        public bool IsSmokingAllowed { get; set; } = true;
    }

    public class RoomRestriction
    {
        public bool Refundable { get; set; } = false;
        public ushort MinStay { get; set; } = 1;
        public ushort MaxStay { get; set; } = 10;
    }

    public class RoomAccessibility
    {
        public bool IsWheelchairFriendly { get; set; } = false;
        public bool HasBrailleSignage { get; set; } = false;
    }
}

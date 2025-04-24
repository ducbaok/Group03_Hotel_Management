using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Models
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
        public ushort MaxOccupancy { get; set; }
        public ushort NumberOfBeds { get; set; }
        public BedDesign BedType { get; set; }
        public ViewType ViewType { get; set; }
    }

    public class RoomAmenity
    {
        public bool HasWifi { get; set; }
        public bool HasTV { get; set; }
        public bool HasBalcony { get; set; }
        public bool HasKitchen { get; set; }
        public bool HasBathTub { get; set; }
        public bool IsSmokingAllowed { get; set; }
    }

    public class RoomRestriction
    {
        public bool Refundable { get; set; }
        public ushort MinStay { get; set; }
        public ushort MaxStay { get; set; }
    }

    public class RoomAccessibility
    {
        public bool IsWheelchairFriendly { get; set; }
        public bool HasBrailleSignage { get; set; }
    }
}

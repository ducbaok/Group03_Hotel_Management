using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using YNL.Utilities.Addons;

namespace YNL.Checkotel
{
    [System.Serializable]
    public class HotelContainer
    {
        public SerializableDictionary<UID, HotelUnit> Hotels = new();    // Key is ID of HotelUnit
    }

    [System.Serializable]
    public class HotelUnit
    {
        public HotelDescription Description = new();
        public HotelReview Review = new();
        public List<RoomUnit> Rooms = new();
        public HotelStatus Status = new();

        public (float Price, Room.StayType Type) HighestPrice = (0, Room.StayType.Hourly);
        public (float Price, Room.StayType Type) LowestPrice = (0, Room.StayType.Hourly);
    }

    [System.Serializable]
    public class HotelDescription
    {
        public string Name = string.Empty;
        public string Address = string.Empty;
        public string Phone = string.Empty;
        public string Email = string.Empty;
        public string Description = string.Empty;
        public string ImageURL;

        public HotelFacility Facilities = HotelFacility.None;
    }

    [System.Serializable]
    public class HotelStatus
    {
        public SerializableDateTime ParticipationDate = new(DateTime.Now);
        public uint ReservationAmount;
    }
}

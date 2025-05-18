﻿using Newtonsoft.Json;
using System;
using UnityEngine;

namespace YNL.Checkotel
{
    [System.Serializable]
    public class RoomDescription
    {
        public string Description = string.Empty;
        public RoomProperty Property = new();
        public RoomFacility Facility = new();
        public RoomRestriction Restriction = new();
        [JsonIgnore] public Texture2D Image;
    }

    [System.Serializable]
    public class RoomProperty
    {
        public ushort MaxOccupancy = 5;
        public ushort NumberOfBeds = 2;
        public Room.BedType BedType = new();
        public Room.ViewType ViewType = Room.ViewType.Ocean;
    }

    [System.Serializable]
    public class RoomRestriction
    {
        public bool Refundable = false;
        public ushort MinStay = 1;
        public ushort MaxStay = 10;
    }

    [Flags]
    public enum RoomFacility : uint
    {
        None = 0,

        HasPrivateWifi = 1 << 0,
        HasTV = 1 << 1,
        HasBalcony = 1 << 2,
        HasKitchen = 1 << 3,
        HasBathTub = 1 << 4,
        HasBrailleSignage = 1 << 5,
        HasAirConditioning = 1 << 6,
        HasHeating = 1 << 7,

        IsPetFriendly = 1 << 8,
        IsSmokingAllowed = 1 << 9,
        IsWheelchairFriendly = 1 << 10,
    }

    [Flags]
    public enum HotelFacility : uint
    {
        None = 0,

        HasPool = 1 << 0,
        HasGym = 1 << 1,
        HasSpa = 1 << 2,
        HasRestaurant = 1 << 3,
        HasBar = 1 << 4,
        HasParrkingLots = 1 << 5,
        HasFreeBreakfast = 1 << 6,
        HasEVChargingStation = 1 << 7,
        HasElevator = 1 << 9,

        IsPetFriendly = 1 << 8,
    }
}

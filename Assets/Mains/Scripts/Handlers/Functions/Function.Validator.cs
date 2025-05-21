using System;
using System.Linq;
using UnityEngine;
using YNL.Utilities.Addons;
using YNL.Utilities.Extensions;

namespace YNL.Checkotel
{
    public static partial class Function
    {
        private static SerializableDictionary<UID, HotelUnit> _hotels => Main.Database.Hotels;

        public static bool IsValidTimeRange(this UID id, Room.StayType type, DateTime checkInTime, byte duration)
        {
            if (checkInTime == DateTime.MinValue) return true;

            var unit = _hotels[id];

            foreach (var room in unit.Rooms)
            {
                var restriction = room.Description.Restriction;
                var (validTime, validStay) = (restriction.ValidTime, restriction.ValidStay);

                bool isValid = type switch
                {
                    Room.StayType.Hourly => checkInTime.Hour >= validTime.TimeIn && checkInTime.Hour < validTime.TimeOut
                                      && duration >= validStay.TimeIn && duration <= validStay.TimeOut,

                    Room.StayType.Overnight => checkInTime.Hour >= validTime.TimeIn
                                          && checkInTime.AddHours(duration).Hour <= validTime.TimeOut
                                          && validStay.TimeIn == 0 && validStay.TimeOut == 0,

                    Room.StayType.Daily => checkInTime.Day >= validTime.TimeIn && checkInTime.Day < validTime.TimeOut
                                      && duration >= validStay.TimeIn && duration <= validStay.TimeOut,

                    _ => false
                };

                if (isValid) return true;
            }

            return false;
        }

        public static (float, Room.StayType) GetHighestPrice(this UID id)
        {
            var unit = _hotels[id];

            if (unit.HighestPrice.Price != 0) return unit.HighestPrice;

            var orderedRooms = unit.Rooms.OrderByDescending(r => r.Price.BasePrice).ToArray();
            unit.HighestPrice.Price = orderedRooms[0].Price.BasePrice;
            unit.HighestPrice.Type = orderedRooms[0].Description.Restriction.StayType;

            return unit.HighestPrice;
        }
        public static (float, Room.StayType) GetLowestPrice(this UID id)
        {
            var unit = _hotels[id];

            if (unit.LowestPrice.Price != 0) return unit.LowestPrice;

            var orderedRooms = unit.Rooms.OrderBy(r => r.Price.BasePrice).ToArray();
            unit.LowestPrice.Price = orderedRooms[0].Price.BasePrice;
            unit.LowestPrice.Type = orderedRooms[0].Description.Restriction.StayType;

            return unit.LowestPrice;
        }

        public static string GetStayTypeUnit(this Room.StayType type, int duration, bool isCapital = false)
        {
            string unit = type switch
            {
                Room.StayType.Hourly => isCapital ? "Hour" : "hour",
                Room.StayType.Overnight => isCapital ? "Night" : "night",
                Room.StayType.Daily => isCapital ? "Day" : "day",
                _ => ""
            };

            return duration <= 1 ? unit : $"{unit}s";
        }
        public static (string Name, Texture2D Icon) GetHotelFacilitiesField(this HotelFacility facility)
        {
            var name = facility.ToString().RemoveWord("Has").RemoveWord("Is").ToSentenceCase();
            var icon = Main.Resources.Icons["Settings"];

            if (Main.Resources.Icons.TryGetValue(name, out Texture2D validIcon))
            {
                icon = validIcon;
            }

            return (name, icon);
        }
    
        public static (TimeRange Hourly, TimeRange Overnight, TimeRange Daily) GetFirstTimeRange(this HotelUnit unit)
        {
            var hourlyTime = new TimeRange();
            var overnightTime = new TimeRange();
            var dailyTime = new TimeRange();

            foreach (var room in unit.Rooms)
            {
                if (room.Description.Restriction.StayType == Room.StayType.Hourly)
                {
                    if (hourlyTime != TimeRange.Zero) continue;
                    hourlyTime = room.Description.Restriction.ValidTime;
                }
                else if (room.Description.Restriction.StayType == Room.StayType.Overnight)
                {
                    if (overnightTime != TimeRange.Zero) continue;
                    overnightTime = room.Description.Restriction.ValidTime;
                }
                else if (room.Description.Restriction.StayType == Room.StayType.Daily)
                {
                    if (dailyTime != TimeRange.Zero) continue;
                    dailyTime = room.Description.Restriction.ValidTime;
                }
            }

            return (hourlyTime, overnightTime, dailyTime);
        }
    }
}

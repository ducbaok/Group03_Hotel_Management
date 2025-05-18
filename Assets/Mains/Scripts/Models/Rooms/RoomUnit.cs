using System;
using System.Collections.Generic;

namespace YNL.Checkotel
{
    [System.Serializable]
    public class RoomCategorization
    {
        public List<RoomUnit> Rooms = new();
    }

    [System.Serializable]
    public class RoomUnit
    {
        public string Name = string.Empty;
        public RoomDescription Description = new();
        public RoomPrice Price = new();
        public ushort RoomAmount = new();
    }

    [System.Serializable]
    public class RoomStatus
    {
        public bool IsAvailable = true;
    }

    [System.Serializable]
    public class RoomPrice
    {
        public uint BasePrice = 0; 
        public uint Discount = 0;   
        public uint FinalPrice
        {
            get
            {
                return Math.Max(BasePrice - Discount, 0);
            }
        }
    }
}

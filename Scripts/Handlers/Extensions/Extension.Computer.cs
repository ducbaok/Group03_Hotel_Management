﻿using HotelReservation.Exceptions;

namespace HotelReservation
{
    public static partial class Extension
    {
        public static class Computer
        {
            public static UID GetNextID(IDType type)
            {
                if (!UID.UIDs.ContainsKey(type))
                {
                    throw new UninitializedSystemException("UID is not initialized yet.");
                }

                return UID.UIDs[type]++;
            }
        }
    }
}
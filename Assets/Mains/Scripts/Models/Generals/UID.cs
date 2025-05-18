using System;
using UnityEngine;

namespace YNL.Checkotel
{
    public enum UIDType : byte
    { 
        Account, Hotel
    }

    [System.Serializable]
    public struct UID
    {
        private static uint _sUID;

        [SerializeField] private uint _id;

        public UID(uint id)
        {
            _id = id;
        }

        public static implicit operator uint(UID id) => id._id;
        public static implicit operator UID(uint id) => new(id);

        public static UID Parse(string id) => new(uint.Parse(id));
        public static UID Create(UIDType type) => new((uint)type * 10000000 + _sUID++);

        public override string ToString() => $"{_id.ToString("D5")}";
        public override bool Equals(object obj)
        {
            if (obj is UID other)
            {
                return _id == other._id;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
    }
}

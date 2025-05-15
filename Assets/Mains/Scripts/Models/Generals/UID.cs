namespace YNL.Checkotel
{
    [System.Serializable]
    public struct UID
    {
        public static UID SUID;

        public uint ID;

        public UID(uint id)
        {
            ID = id;
        }

        public static implicit operator uint(UID id) => id.ID;
        public static implicit operator UID(uint id) => new(id);

        public static UID Parse(string id) => new(uint.Parse(id));
        public static UID Create() => new(SUID++);

        public override string ToString() => $"{ID}";
        public override bool Equals(object? obj)
        {
            if (obj is UID other)
            {
                return ID == other.ID;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}

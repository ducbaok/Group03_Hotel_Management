namespace YNL.Checkotel
{
    [System.Serializable]
    public class Account
    {
        public UID ID = UID.Create();
        public string Name = string.Empty;
        public string Email = string.Empty;
        public string PhoneNumber = string.Empty;
        public string Password = string.Empty;
    }
}

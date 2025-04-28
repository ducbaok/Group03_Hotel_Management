using HotelReservation.Utils;

namespace HotelReservation
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            //DataGenerator.PrintRoomDescription(DataGenerator.GenerateRandomRoomDescription());

            var DataContainer = new DataContainer();

            DataContainer.Accounts = CsvSerializer.LoadFromCsv(@"C:\Users\Yunasawa\Documents\Projects\G3 Hotel Reservation\Datas\AccountData.csv");
            DataContainer.SignAccount(AccountSignType.SignUp, AccountVerificationType.PhoneNumber, "6754483676", "mwU6cwgS9zeN", "mwU6cwgS9zeN");
        }
    }
}
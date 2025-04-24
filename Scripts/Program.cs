using HotelReservation.Utils;

namespace HotelReservation
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            System.Console.WriteLine("Hello, World!");
            // Account
            List<Account> testAccounts = DataGenerator.GenerateAccountsInRange(1, 20, true);
            foreach (var acc in testAccounts){
                Console.WriteLine($"ID={(uint)acc.ID}, Name={acc.Name}, Email={acc.Email}, Phone={acc.Phone}, Password={acc.Password}");
            }
            
            // RoomDescription
            DataGenerator.PrintRoomDescription(DataGenerator.GenerateRandomRoomDescription());
        }
    }
}
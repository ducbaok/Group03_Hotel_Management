using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace YNL.Checkotel
{
    public static partial class Function
    {
        public static string[] SplitCSV(this string csvData)
        {
            var pattern = @"(?:^|,)(?:""([^""]*)""|([^,]*))";
            var matches = Regex.Matches(csvData, pattern);

            List<string> fields = new List<string>();

            foreach (Match match in matches)
            {
                string value = match.Groups[1].Success ? match.Groups[1].Value : match.Groups[2].Value;
                fields.Add(value);
            }

            return fields.ToArray();
        }

        public static void SerializeHotelDatabse(this DatabaseContainerSO _database, string[] fields)
        {
            var uid = UID.Parse(fields[0]);
            var unit = new HotelUnit();

            _database.Hotels[uid] = unit;

            unit.Description.Name = fields[1];
            unit.Description.Address = fields[2];
            unit.Description.Phone = fields[3];
            unit.Description.Email = fields[4];
            unit.Description.Description = fields[5];
            unit.Description.ImageURL = fields[6];
            unit.Status.ParticipationDate = new(fields[7]);
            unit.Description.Facilities = fields[8].ToFacilities();
            unit.Description.Policy = fields[9];
            unit.Description.Cancellation = fields[10];
            if (uint.TryParse(fields[12], out uint result))
            {
                unit.Status.ReservationAmount = result;
            }
            else
            {
                unit.Status.ReservationAmount = 0;
            }
        }
    }
}
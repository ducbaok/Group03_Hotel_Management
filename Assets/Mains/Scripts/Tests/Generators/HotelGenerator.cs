using System.IO;
using System;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Text.RegularExpressions;
using YNL.Utilities.Extensions;
using System.Collections.Generic;

namespace YNL.Checkotel
{
    public class HotelGenerator : MonoBehaviour
    {
        [SerializeField] private string _url;
        [SerializeField] private DatabaseContainerSO _database;

        [Button]
        public async void Generate()
        {
            var content = await _url.GetRawDatabaseAsync();

            _database.Hotels.Clear();

            string[] lines = content.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            for (int l = 1; l < lines.Length; l++)
            {
                GenerateHotel(ParseCsv(lines[l]));
            }
        }

        private void GenerateHotel(string[] fields)
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
            unit.Description.Facilities = ConvertToFacilities(fields[8]);
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

        private HotelFacility ConvertToFacilities(string input)
        {
            HotelFacility result = HotelFacility.None;

            foreach (string part in input.Split(';'))
            {
                string trimmed = part.Trim();
                if (Enum.TryParse<HotelFacility>(trimmed, ignoreCase: true, out var facility))
                {
                    result |= facility;
                }
                else
                {
                    Console.WriteLine($"Warning: Unknown facility '{trimmed}'");
                }
            }

            return result;
        }

        public static string[] ParseCsv(string csvData)
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
    }
}
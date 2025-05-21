using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace YNL.Checkotel
{
    public static partial class Function
    {
        public static bool FuzzyContains(this string source, string target, int maxDistance = 1)
        {
            if (string.IsNullOrEmpty(target)) return true;

            int targetLength = target.Length;

            for (int i = 0; i <= source.Length - targetLength; i++)
            {
                string substring = source.Substring(i, targetLength);

                if (LevenshteinDistance(substring, target) <= maxDistance)
                {
                    return true;
                }
            }

            return false;

            int LevenshteinDistance(string s, string t)
            {
                if (s == t) return 0;
                if (s.Length == 0) return t.Length;
                if (t.Length == 0) return s.Length;

                int[,] distance = new int[s.Length + 1, t.Length + 1];

                for (int i = 0; i <= s.Length; i++) distance[i, 0] = i;
                for (int j = 0; j <= t.Length; j++) distance[0, j] = j;

                for (int i = 1; i <= s.Length; i++)
                {
                    for (int j = 1; j <= t.Length; j++)
                    {
                        int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;
                        distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                    }
                }

                return distance[s.Length, t.Length];
            }
        }
        
        public static (string In, string Out) GetCheckingTimeText(this DateTime checkInTime, byte duration)
        {
            string checkInText = checkInTime == DateTime.MinValue ? "Anytime" : $"{checkInTime:HH:mm}, {checkInTime:dd/MM}";

            DateTime checkOutTime = checkInTime.AddHours(duration);
            string checkOutText = checkInTime == DateTime.MinValue ? "Anytime" : $"{checkOutTime:HH:mm}, {checkOutTime:dd/MM}";

            return (checkInText, checkOutText);
        }

        public static List<HotelFacility> GetHotelFacilities(this HotelUnit unit, int max = 5)
        {
            var selectedFacilities = new List<HotelFacility>();
            var facilities = unit.Description.Facilities;

            foreach (HotelFacility facility in Enum.GetValues(typeof(HotelFacility)))
            {
                if (facility == HotelFacility.None) continue;

                if (facilities.HasFlag(facility))
                {
                    selectedFacilities.Add(facility);
                    if (selectedFacilities.Count >= max) break;
                }
            }

            return selectedFacilities;
        }

        public static string ToSentenceCase<T>(this T input)
        {
            string spaced = Regex.Replace(input.ToString(), "(?<!^)([A-Z](?=[a-z])|(?<=[a-z])[A-Z])", " $1");

            string[] words = spaced.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (!Regex.IsMatch(words[i], @"^[A-Z]+$"))
                {
                    words[i] = words[i].ToLower();
                }
            }

            TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
            words[0] = textInfo.ToTitleCase(words[0]);

            return string.Join(" ", words);
        }

        public static string ToDateFormat(this byte number)
        {
            if (number <= 0) return number.ToString();

            int lastDigit = number % 10;
            int lastTwoDigits = number % 100;

            string suffix = (lastTwoDigits >= 11 && lastTwoDigits <= 13) ? "th"
                          : (lastDigit == 1) ? "st"
                          : (lastDigit == 2) ? "nd"
                          : (lastDigit == 3) ? "rd"
                          : "th";

            return number + suffix;
        }


    }
}
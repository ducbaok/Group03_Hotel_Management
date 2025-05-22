using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

namespace YNL.Checkotel
{
    public static partial class Function
    {
        public static string ToRank(this ReviewRating rating)
        {
            if (rating >= 0 && rating < 1) return "Disappointing";
            else if (rating >= 1 && rating < 2) return "Moderate";
            else if (rating >= 2 && rating < 3) return "Adequate";
            else if (rating >= 3 && rating < 4) return "Impressive";
            else if (rating >= 4 && rating < 5) return "Exceptional";
            return "";
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

        public static HotelFacility ToFacilities(this string input)
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
        
        public static HotelFacility[] ToArray(this HotelFacility facilities)
        {
            List<HotelFacility> facilityArray = new List<HotelFacility>();

            foreach (HotelFacility facility in Enum.GetValues(typeof(HotelFacility)))
            {
                if (facility != HotelFacility.None && facilities.HasFlag(facility))
                {
                    facilityArray.Add(facility);
                }
            }

            return facilityArray.ToArray();
        }
    }
}
using System;

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
    }
}
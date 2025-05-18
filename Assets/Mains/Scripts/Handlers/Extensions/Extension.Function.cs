using Cysharp.Threading.Tasks;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UIElements;
using YNL.Utilities.UIToolkits;

namespace YNL.Checkotel
{
    public static partial class Extension
    {
        public static class Function
        {
            public static string AddSpace<T>(T input)
            {
                return Regex.Replace(input.ToString(), "(?<!^)([A-Z])", " $1");
            }

            public static string ToSentenceCase<T>(T input)
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

            public static float ToClosestPrice(float value)
            {
                float roundedValue = (float)Math.Round(value, 1);

                return roundedValue - 0.01f;
            }

            public static float AddAverage(float previousAverage, int count, float newValue)
            {
                return (previousAverage * count + newValue) / (count + 1);
            }

            public static bool FuzzyContains(string source, string target, int maxDistance = 1)
            {
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

            public static void ApplyClouldImageAsync(VisualElement element, string url)
            {
                ApplyClouldImage(element, url).Forget();

                async UniTaskVoid ApplyClouldImage(VisualElement element, string url)
                {
                    using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
                    {
                        var operation = uwr.SendWebRequest();
                        await UniTask.WaitUntil(() => operation.isDone);

                        if (uwr.result == UnityWebRequest.Result.Success)
                        {
                            Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                            element.SetBackgroundImage(texture);
                        }
                        else
                        {
                            Debug.LogError($"Failed to load texture: {uwr.error}");
                        }
                    }
                }
            }
        }
    }
}

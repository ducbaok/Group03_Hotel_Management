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

            

            public static float ToClosestPrice(float value)
            {
                float roundedValue = (float)Math.Round(value, 1);

                return roundedValue - 0.01f;
            }

            public static float AddAverage(float previousAverage, int count, float newValue)
            {
                return (previousAverage * count + newValue) / (count + 1);
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

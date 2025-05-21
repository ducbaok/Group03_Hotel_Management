using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using YNL.Utilities.Extensions;

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

        public static async UniTask<Texture2D[]> GetRoomImageAsync(this HotelUnit unit)
        {
            var urls = unit.Rooms?.Where(i => i.Description != null && !string.IsNullOrEmpty(i.Description.ImageURL)).Select(i => i.Description.ImageURL).ToArray();
            var tasks = new List<UniTask<Texture2D>>();

            foreach (string url in urls)
            {
                tasks.Add(DownloadImageAsync(url));
            }

            return await UniTask.WhenAll(tasks);

            async UniTask<Texture2D> DownloadImageAsync(string url)
            {
                using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
                {
                    await uwr.SendWebRequest();

                    if (uwr.result != UnityWebRequest.Result.Success)
                    {
                        Debug.LogError($"Failed to download image: {url}, Error: {uwr.error}");
                        return null;
                    }

                    return DownloadHandlerTexture.GetContent(uwr);
                }
            }
        }
    }
}
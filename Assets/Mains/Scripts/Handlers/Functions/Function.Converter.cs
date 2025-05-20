using UnityEngine;
using YNL.Utilities.Extensions;

namespace YNL.Checkotel
{
    public static partial class Function
    {
        public static (string Name, Texture2D Icon) GetHotelFacilitiesField(this HotelFacility facility)
        {
            var name = facility.ToString().RemoveWord("Has").RemoveWord("Is").ToSentenceCase();
            var icon = Main.Resources.Icons["Settings"];

            if (Main.Resources.Icons.TryGetValue(name, out Texture2D validIcon))
            {
                icon = validIcon;
            }

            return (name, icon);
        }

        public static string ToRank(this ReviewRating rating)
        {
            if (rating >= 0 && rating < 1) return "Disappointing";
            else if (rating >= 1 && rating < 2) return "Moderate";
            else if (rating >= 2 && rating < 3) return "Adequate";
            else if (rating >= 3 && rating < 4) return "Impressive";
            else if (rating >= 4 && rating < 5) return "Exceptional";
            return "";
        }
    }
}
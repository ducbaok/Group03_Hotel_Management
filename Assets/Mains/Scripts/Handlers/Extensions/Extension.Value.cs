using System.Text.RegularExpressions;

namespace YNL.Checkotel
{
    public static partial class Extension
    {
        public static class Value
        {
            public static string AddSpace<T>(T input)
            {
                return Regex.Replace(input.ToString(), "(?<!^)([A-Z])", " $1");
            }
        }
    }
}

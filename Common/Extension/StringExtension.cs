using System.Text.RegularExpressions;

namespace Common.Extension
{
    public static class StringExtension
    {
        public static string RemoveSubstrings(this string mainString, params string[] subStrings)
        {
            var text = mainString;
            foreach (var sub in subStrings)
            {
                text = text.Replace(sub, string.Empty);
            }

            return text;
        }

        public static string RemoveHtmlTags(this string source)
        {
            var result = Regex.Replace(source, @"<[^>]+>|&nbsp;", "").Trim();
            return Regex.Replace(result, @"\s{2,}", " ");
        }
    }
}

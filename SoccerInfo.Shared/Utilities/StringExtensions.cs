using System.Globalization;

namespace SoccerInfo.Shared.Utilities;
public static class StringExtensions
{
    public static float ParseToFloat(this string text) => float.Parse(text);
    public static int ParseToInt(this string text) => int.Parse(text);
    public static DateTime ParseToDate(this string text) => DateTime.Parse(text);
    public static float? TryParseToFloat(this string text)
    {
        if (float.TryParse(text, CultureInfo.GetCultureInfo("pl-PL"), out var result))
            return result;
        else
            return null;
    }

    public static int? TryParseToInt(this string text)
    {
        if (int.TryParse(text, CultureInfo.GetCultureInfo("pl-PL"), out var result))
            return result;
        else
            return null;
    }

    public static DateTime? TryParseToDate(this string text)
    {
        if (DateTime.TryParse(text, out var result))
            return result;
        else
            return null;
    }


    public static string CapitalizeFirstLetter(this string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        return char.ToUpper(text[0]) + text.Substring(1);
    }
}

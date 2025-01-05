namespace SoccerInfo.FrontendScraper.Utilities;

internal static class StringExtensions
{
    public static string FormatExtractedString(this string text)
    {
        text = text.Replace("&nbsp;", " ");
        text = text.Replace("&nbsp", " ");
        text = text.Replace("&amp;", "&");
        text = text.Replace("&amp", "&");

        return text.Trim();
    }
}

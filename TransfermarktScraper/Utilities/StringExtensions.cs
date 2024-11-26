using System.Runtime.CompilerServices;

namespace TransfermarktScraper.Utilities;

internal static class StringExtensions
{
    public static string FormatExtractedStrings(this string text)
    {
        if (text.Contains("&nbsp"))
            text = text.Split("&nbsp")[0];

        return text.Trim();
    }
}

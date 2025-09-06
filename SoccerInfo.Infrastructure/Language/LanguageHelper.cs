using System.Globalization;

namespace SoccerInfo.Infrastructure.Language;

internal static class LanguageHelper
{
    public static void SetCultureToPL()
    {
        var culture = new CultureInfo("pl-PL");
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }
}

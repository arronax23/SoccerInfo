using Microsoft.Extensions.Hosting;
using Microsoft.Playwright;

namespace SoccerInfo.FrontendScraper.Utilities;
public class CookieReader(IHostEnvironment hostEnvironment)
{
    [Obsolete]
    public Cookie[] ReadFromJsonFile()
    {
        var jsonString = File.ReadAllText("./Cookies.json");

        return System.Text.Json.JsonSerializer.Deserialize<Cookie[]>(jsonString)!;
    }
}

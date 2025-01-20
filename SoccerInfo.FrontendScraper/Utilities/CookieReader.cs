using Microsoft.Extensions.Hosting;
using Microsoft.Playwright;

namespace SoccerInfo.FrontendScraper.Utilities;
public class CookieReader(IHostEnvironment hostEnvironment)
{
    public Cookie[] ReadFromJsonFile()
    {
        var jsonString = string.Empty;

        if (hostEnvironment.IsProduction())
            jsonString = File.ReadAllText("./Cookies.json");
        else
            jsonString = File.ReadAllText("./../SoccerInfo.FrontendScraper/Cookies.json");

        return System.Text.Json.JsonSerializer.Deserialize<Cookie[]>(jsonString)!;
    }
}

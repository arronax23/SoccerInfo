using Microsoft.Playwright;

namespace SoccerInfo.FrontendScraper.Utilities;
public class CookieReader
{
    //public CookieParam[] ReadFromJsonFile()
    //{
    //    string jsonString = File.ReadAllText("./../SoccerInfo.FrontendScraper/Cookies.json");
    //    return System.Text.Json.JsonSerializer.Deserialize<CookieParam[]>(jsonString)!;
    //}
    public Cookie[] ReadFromJsonFile()
    {
        string jsonString = File.ReadAllText("./../SoccerInfo.FrontendScraper/Cookies.json");
        return System.Text.Json.JsonSerializer.Deserialize<Cookie[]>(jsonString)!;
    }


}

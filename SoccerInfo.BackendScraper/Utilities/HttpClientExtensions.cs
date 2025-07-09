using System.Net.Http.Headers;

namespace SoccerInfo.BackendScraper.Utilities;
internal static class HttpClientExtensions
{
    public static void AddHeadersForScrape(this HttpClient client)
    {
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
}

using System.Text.Json;

namespace SoccerInfo.Shared.Utilities;
public static class JsonSerializerToFile
{
    public static async Task Save<T>(T data, string filePath)
    {
        var json = JsonSerializer.Serialize(data, options: new JsonSerializerOptions()
        {
            WriteIndented = true,
        });

        using TextWriter tw = new StreamWriter(filePath);
        await tw.WriteAsync(json);
    }
}

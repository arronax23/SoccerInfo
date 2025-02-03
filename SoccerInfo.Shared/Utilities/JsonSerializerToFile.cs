using Serilog;
using System.Text.Json;

namespace SoccerInfo.Shared.Utilities;
public static class JsonSerializerToFile
{
    public static async Task<bool> Save<T>(T data, string fileName)
    {
        var json = JsonSerializer.Serialize(data, options: new JsonSerializerOptions()
        {
            WriteIndented = true,
        });

        try
        {
            using (FileStream fs = new FileStream(fileName, FileMode.CreateNew))
            using (StreamWriter writer = new StreamWriter(fs))
                await writer.WriteAsync(json);

            return true;
        }
        catch(IOException ex)
        {
            Log.Logger.Information(ex.Message);
            return false;
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex.ToString());
            return false;
        }
    }
}

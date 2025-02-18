using Serilog;
using System.Text.Json;

namespace SoccerInfo.Shared.Utilities;
public static class JsonSerializerToFile
{
    public static async Task<bool> Save<T>(T data, string fileName, bool overwrite = false)
    {
        var json = JsonSerializer.Serialize(data, options: new JsonSerializerOptions()
        {
            WriteIndented = true,
        });

        var fileMode = overwrite ? FileMode.Create : FileMode.CreateNew;

        try
        {
            using (FileStream fs = new FileStream(fileName, fileMode))
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

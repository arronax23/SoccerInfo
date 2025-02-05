using SoccerInfo.Shared.Utilities;
using System.Text;

namespace SoccerInfo.Persistence.JsonFileData;

public class JsonFileDataManager
{
    private const string DIRECTORY_NAME = "JsonData";  
    public async Task SaveData<T>(T data, string fileName)
    {
        VerifyDirectoryPresence(DIRECTORY_NAME);

        var isSuccess = false;
        var index = 0;

        while (isSuccess == false) 
        {
            var modifiedFileName = AddIndexSuffix(fileName, ++index);
            isSuccess = await JsonSerializerToFile.Save(data, Path.Combine(DIRECTORY_NAME, modifiedFileName));
        }
    }

    private void VerifyDirectoryPresence(string directoryName)
    {
        if (!Directory.Exists(directoryName))
            Directory.CreateDirectory(directoryName);
    }

    private string AddIndexSuffix(string fileName, int index)
        => new StringBuilder(fileName).Append($"_{index}.json").ToString();

}

using SoccerInfo.Application.Abstractions.Interfaces;
using SoccerInfo.Shared.Utilities;
using System.Text;

namespace SoccerInfo.Persistence.JsonFileData;

public class JsonFileDataManager : IJsonFileDataManager
{
    private const string DIRECTORY_NAME = "JsonData";
    public async Task<string> SaveData<T>(T data, string fileName)
    {
        VerifyDirectoryPresence(DIRECTORY_NAME);

        var fullFileName = string.Empty;
        var isSuccess = false;
        var index = 0;

        while (isSuccess == false)
        {
            fullFileName = AddIndexSuffix(fileName, ++index);
            isSuccess = await JsonSerializerToFile.Save(data, Path.Combine(DIRECTORY_NAME, fullFileName));
        }

        return fullFileName;
    }

    private void VerifyDirectoryPresence(string directoryName)
    {
        if (!Directory.Exists(directoryName))
            Directory.CreateDirectory(directoryName);
    }

    private string AddIndexSuffix(string fileName, int index)
        => new StringBuilder(fileName).Append($"_{index}.json").ToString();
}

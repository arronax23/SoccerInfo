
namespace SoccerInfo.Application.JsonFileData;

public interface IJsonFileDataManager
{
    Task SaveData<T>(T data, string fileName);
}
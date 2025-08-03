namespace SoccerInfo.Application.Interfaces;

public interface IJsonFileDataManager
{
    Task SaveData<T>(T data, string fileName);
}
namespace SoccerInfo.Application.Abstractions.Interfaces;

public interface IJsonFileDataManager
{
    Task SaveData<T>(T data, string fileName);
}
namespace SoccerInfo.Application.Abstractions.Interfaces;

public interface IJsonFileDataManager
{
    Task<string> SaveData<T>(T data, string fileName);
}
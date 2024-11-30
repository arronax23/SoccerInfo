namespace SoccerInfo.Shared.CQRS;
public interface IQueryDispatcher
{
    Task<TDto> Send<TDto>(IQuery<TDto> command);
}
namespace SoccerInfo.Shared.CQRS;
public interface ICommandDispatcher
{
    Task Send(ICommand command);
    Task Send(ICommand command, CancellationToken cancellationToken);
    Task<TDto> Send<TDto>(ICommand<TDto> query);
    Task<TDto> Send<TDto>(ICommand<TDto> query, CancellationToken cancellationToken);
}
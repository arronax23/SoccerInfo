namespace SoccerInfo.Shared.CQRS;
public interface ICommandDispatcher
{
    Task Send(ICommand command);
    Task Send(ICommand command, CancellationToken cancellationToken);
}
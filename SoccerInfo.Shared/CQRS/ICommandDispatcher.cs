namespace SoccerInfo.Shared.CQRS;
public interface ICommandDispatcher
{
    Task Send(ICommand command);
}
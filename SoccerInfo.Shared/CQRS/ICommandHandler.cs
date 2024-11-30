using MediatR;
namespace SoccerInfo.Shared.CQRS;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
{
}
public interface ICommandHandler<TCommand, TDto> : IRequestHandler<TCommand, TDto>
        where TCommand : ICommand<TDto>
{
}

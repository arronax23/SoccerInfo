using MediatR;

namespace SoccerInfo.Shared.CQRS;
public interface ICommand : IRequest
{
}

public interface ICommand<TDto> : IRequest<TDto>
{
}


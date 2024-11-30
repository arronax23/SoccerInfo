using MediatR;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Infrastructure.CQRS;

public class QueryDispatcher(IMediator mediator) : IQueryDispatcher
{
    public async Task<TDto> Send<TDto>(IQuery<TDto> command)
    {
        return await mediator.Send(command);
    }
}

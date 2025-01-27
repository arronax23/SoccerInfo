using MediatR;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Infrastructure.CQRS;

public class QueryDispatcher(IMediator mediator) : IQueryDispatcher
{
    public async Task<TDto> Send<TDto>(IQuery<TDto> query)
    {
        return await mediator.Send(query);
    }

    public async Task<TDto> Send<TDto>(IQuery<TDto> query, CancellationToken cancellationToken)
    {
        return await mediator.Send(query, cancellationToken);
    }
}

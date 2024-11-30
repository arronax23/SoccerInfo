using MediatR;

namespace SoccerInfo.Shared.CQRS;
public interface IQueryHandler<TQuery, TDto> : IRequestHandler<TQuery, TDto>
        where TQuery : IQuery<TDto>
{
}

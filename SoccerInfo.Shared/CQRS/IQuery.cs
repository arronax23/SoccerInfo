using MediatR;

namespace SoccerInfo.Shared.CQRS;
public interface IQuery<TDto> : IRequest<TDto>
{
}

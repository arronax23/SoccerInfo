using SoccerInfo.Domain.Models;

namespace SoccerInfo.Application.Services;

public interface IGeneralPositionService
{
    Task AttachGeneralPosition(Player player);
}

using Microsoft.AspNetCore.Mvc;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Application.Queries.GetSoccerDataAmount;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfoWeb.API.Controllers;

[ApiController]
public class HomeController(IQueryDispatcher queryDispatcher) : ControllerBase
{
    [HttpGet("api/GetSoccerDataAmount")]
    public async Task<SoccerDataAmountDto> GetSoccerDataAmount()
    {
        return await queryDispatcher.Send(new GetSoccerDataAmountQuery());
    }
}

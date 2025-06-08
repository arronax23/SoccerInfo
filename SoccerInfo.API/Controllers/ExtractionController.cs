using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SoccerInfo.API.Authorization;
using SoccerInfo.Application.Commands.ExtarctPlayersCharacteristics;
using SoccerInfo.Application.Commands.ExtarctPlayersGeneralnfo;
using SoccerInfo.Application.Commands.ExtractMarketValueProgress;
using SoccerInfo.Application.Commands.ExtractNationalities;
using SoccerInfo.Application.Commands.SavePlayersCharacteristicsFromFile;
using SoccerInfo.Application.Commands.SavePlayersGeneralInfoFromFile;
using SoccerInfo.Application.Commands.SaveTransfermarktCookie;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfoWeb.API.Controllers;

[ApiController]
[ApiKeyAuthorizationFilter]
public class ExtractionController(ICommandDispatcher commandDispatcher) : ControllerBase
{
    [HttpPut("api/ExtarctPlayersGeneralnfo")]
    public async Task<IActionResult> ExtarctPlayers()
    {
        await commandDispatcher.Send(new ExtarctPlayersGeneralnfoCommand());
        return Ok();
    }

    [HttpPut("api/ExtarctPlayersCharacteristics")]
    public async Task<IActionResult> ExtarctPlayersCharacteristics(
        int playerCount, 
        bool onlyNewPlayers, 
        CancellationToken cancellationToken)
    {
        await commandDispatcher.Send(new ExtarctPlayersCharacteristicsCommand()
        {
            PlayerCount  = playerCount,
            OnlyNewPlayers = onlyNewPlayers
        }, 
        cancellationToken);
        return Ok();
    }

    [HttpPut("api/SavePlayersCharacteristicsFromFile")]
    public async Task<IActionResult> SavePlayersCharacteristicsFromFile(string fileName)
    {
        if (fileName.IsNullOrEmpty())
            return BadRequest();

        await commandDispatcher.Send(new SavePlayersCharacteristicsFromFileCommand()
        {
            FileName = fileName
        });
        return Ok();
    }

    [HttpPut("api/SaveTransfermarktCookie")]
    public async Task<IActionResult> SaveTransfermarktCookie()
    {
        await commandDispatcher.Send(new SaveTransfermarktCookieCommand());
        return Ok();
    }

    [HttpPut("api/SavePlayersGeneralInfoFromFile")]
    public async Task<IActionResult> SavePlayersGeneralInfo(string fileName)
    {
        if (fileName.IsNullOrEmpty())
            return BadRequest();

        await commandDispatcher.Send(new SavePlayersGeneralInfoFromFileCommand()
        {
            FileName = fileName
        });

        return Ok();
    }

    [HttpPut("api/ExtractMarketValueProgress")]
    public async Task<IActionResult> ExtractMarketValueProgress()
    {
        await commandDispatcher.Send(new ExtractMarketValueProgressCommand());
        return Ok();
    }



    [HttpPut("api/ExtractNationalities")]
    public async Task<IActionResult> ExtractNationalities()
    {
        await commandDispatcher.Send(new ExtarctNationalitiesCommand());
        return Ok();
    }
}

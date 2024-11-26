using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransfermarktScraper;
using TransfermarktScraperWeb.Server.Data;
using TransfermarktScraperWeb.Server.Data.Models;

namespace TransfermarktScraperWeb.Server.Controllers;

[ApiController]
public class PlayersController(
    TransfermarktExtractor transfermarktExtractor,
    ApplicationDbContext dbContext,
    IMapper mapper) : ControllerBase
{
    [HttpPost]
    [Route("api/ExtarctPlayers")]
    public async Task<IActionResult> ExtarctPlayers()
    {
        var extraction = await transfermarktExtractor.Extarct();

        var teams = extraction.Extraction.Select(x => new Team()
        {
            Name = x.TeamName,
            Players = mapper.Map<ICollection<Player>>(x.Players)
        });


        dbContext.Teams.AddRange(teams);
        dbContext.SaveChanges();

        return Ok();
    }

    [HttpGet("api/GetTeams")]
    public IEnumerable<Team> GetTeams()
    {
        return dbContext.Teams
            .Include(x => x.Players)
            !.ThenInclude(y => y.NationalityImageBase64Collection)
            .AsEnumerable();
    }
}

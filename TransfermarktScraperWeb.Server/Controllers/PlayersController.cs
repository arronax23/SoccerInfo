using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransfermarktScraper;
using TransfermarktScraperWeb.Server.Controllers.Dtos;
using TransfermarktScraperWeb.Server.Data;
using TransfermarktScraperWeb.Server.Data.Models;
using TransfermarktScraperWeb.Server.Sql;
using static TransfermarktScraperWeb.Server.Controllers.Dtos.PlayerDto;

namespace TransfermarktScraperWeb.Server.Controllers;

[ApiController]
public class PlayersController(
    TransfermarktExtractor transfermarktExtractor,
    ApplicationDbContext dbContext,
    ISqlExecutor sqlExecutor,
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
    public IEnumerable<TeamDto> GetTeams()
    {
        return sqlExecutor
            .SqlQuery<TeamDto>(@"SELECT [Id], [Name] FROM Teams");
    }
    [HttpGet("api/GetPlayers/{teamId}")]
    public IEnumerable<PlayerDto> GetPlayers(int teamId)
    {
        return sqlExecutor
            .SqlQueryRaw<PlayerData>(
            $@"SELECT p.[Id], p.[Name], p.[Position], p.[FaceImageBase64], ni.[Base64Image] as NationalityImage FROM Players p
            JOIN Teams t ON t.Id = p.TeamId
            JOIN NationalityImages ni ON ni.PlayerId = p.Id
            WHERE t.Id = {teamId}")
            .ToList()
            .GroupBy(x => x.Id)
            .Select(y => new PlayerDto()
            {
                Id = y.Key,
                Name = y.First().Name,
                FaceImageBase64 = y.First().FaceImageBase64,
                Position = y.First().Position,
                NationalityImageBase64Collection = y.Select(z => z.NationalityImage),
            });
    }

    public class PlayerData
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string? FaceImageBase64 { get; set; }
        public string? NationalityImage { get; set; }

    }

}

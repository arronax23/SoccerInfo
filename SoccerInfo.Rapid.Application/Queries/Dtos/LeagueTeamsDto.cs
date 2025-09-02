
namespace SoccerInfo.Rapid.Application.Queries.Dtos;
public class LeagueTeamsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public IEnumerable<TeamDto> Teams { get; set; } = null!;

    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int PlayersCount { get; set; }
        public string? LogoUrl { get; set; } = null!;
    }
}

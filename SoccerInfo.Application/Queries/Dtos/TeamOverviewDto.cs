namespace SoccerInfo.Application.Queries.Dtos;
public class TeamOverviewDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? TeamImageBase64 { get; set; }
    public int TeamId { get; set; }
    public int LeagueId { get; set; }
    public string? LeagueImageBase64 { get; set; }
}
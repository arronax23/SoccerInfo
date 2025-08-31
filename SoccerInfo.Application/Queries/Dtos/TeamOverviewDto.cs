namespace SoccerInfo.Application.Queries.Dtos;
public class TeamOverviewDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? TeamLogo { get; set; }
    public string? TeamLogoMimeType { get; set; }
    public string? LeagueLogo { get; set; }
    public string? LeagueLogoMimeType { get; set; }
    public int TeamId { get; set; }
    public int LeagueId { get; set; }
}
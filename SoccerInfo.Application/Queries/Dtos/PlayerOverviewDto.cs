namespace SoccerInfo.Application.Queries.Dtos;

public class PlayerOverviewDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? FaceImage { get; set; }
    public string? FaceImageMimeType { get; set; }
    public int TeamId { get; set; }
    public string? TeamLogo { get; set; }
    public string? TeamLogoMimeType { get; set; }
    public int LeagueId { get; set; }
    public string? LeagueLogo { get; set; }
    public string? LeagueLogoMimeType { get; set; }
}

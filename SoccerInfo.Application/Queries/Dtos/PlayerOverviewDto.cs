namespace SoccerInfo.Application.Queries.Dtos;

public class PlayerOverviewDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? FaceImageBase64 { get; set; }
    public int TeamId { get; set; }
    public string? TeamImageBase64 { get; set; }
    public int LeagueId { get; set; }
    public string? LeagueImageBase64 { get; set; }
}

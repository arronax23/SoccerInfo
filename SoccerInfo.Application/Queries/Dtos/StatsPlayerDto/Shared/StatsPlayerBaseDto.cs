namespace SoccerInfo.Application.Queries.Dtos.StatsPlayerDto.Shared;
public abstract class StatsPlayerBaseDto
{
    public int Id { get; set; }
    public int Index { get; set; }
    public string Name { get; set; } = null!;
    public string? FaceImageBase64 { get; set; }
    public int TeamId { get; set; }
    public string? TeamImageBase64 { get; set; }
    public int LeagueId { get; set; }
    public string? LeagueImageBase64 { get; set; }
}

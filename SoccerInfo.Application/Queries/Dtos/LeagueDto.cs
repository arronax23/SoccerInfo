namespace SoccerInfo.Application.Queries.Dtos;

public class LeagueDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? LeagueImageBase64 { get; set; }
    public string? CountryFlagBase64 { get; set; }
}

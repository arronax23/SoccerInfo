namespace SoccerInfo.Application.Queries.Dtos;

public class LeagueDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? LogoBase64 { get; set; }
    public string? LogoMimeType { get; set; }
    public string? CountryFlagBase64 { get; set; }
    public string? CountryFlagMimeType { get; set; }
}

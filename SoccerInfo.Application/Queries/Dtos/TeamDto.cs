namespace SoccerInfo.Application.Queries.Dtos;

public class TeamDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? LogoBase64 { get; set; }
    public string? LogoMimeType { get; set; }
}

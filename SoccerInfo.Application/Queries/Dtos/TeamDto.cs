namespace SoccerInfo.Application.Queries.Dtos;

public class TeamDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? TeamImageBase64 { get; set; }
}


namespace SoccerInfo.Rapid.Application.Queries.Dtos;
public class LeagueDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? LeagueImageUrl { get; set; }
    public string? CountryFlagUrl { get; set; }
}

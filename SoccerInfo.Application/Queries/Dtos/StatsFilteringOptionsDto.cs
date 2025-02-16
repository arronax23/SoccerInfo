namespace SoccerInfo.Application.Queries.Dtos;

public class StatsFilteringOptionsDto
{
    public IEnumerable<string> Positions { get; set; } = null!;
    public IEnumerable<string> Teams { get; set; } = null!;
    public IEnumerable<string> Leagues { get; set; } = null!;
    public IEnumerable<string> Nationalities { get; set; } = null!;
}

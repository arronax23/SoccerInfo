
namespace SoccerInfo.Rapid.Application.Queries.Dtos;
public class PlayerOverviewDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string? FaceImageUrl { get; set; }
    public TeamDto Team { get; set; } = null!;
    public LeagueDto League { get; set; } = null!;

    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? LogoUrl { get; set; }
    }

    public class LeagueDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? LogoUrl { get; set; }
    }
}

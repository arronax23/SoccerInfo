
namespace SoccerInfo.Rapid.Application.Queries.Dtos;
public class PlayerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? Age { get; set; }
    public string MarketValue { get; set; } = null!; 
    public TeamOverviewDto Team { get; set; } = null!;
    public IEnumerable<NationalityDto> Nationalities { get; set; } = null!;

    public class TeamOverviewDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LogoUrl { get; set; } = null!;
    }
}

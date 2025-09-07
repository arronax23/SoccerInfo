
namespace SoccerInfo.Rapid.Application.Queries.Dtos;
public class PlayerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? Age { get; set; }
    public string MarketValue { get; set; } = null!; 
    public ImageDto? FaceImage { get; set; }
    public TeamDto Team { get; set; } = null!;
    public IEnumerable<NationalityDto> Nationalities { get; set; } = null!;

    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LogoUrl { get; set; } = null!;
    }
}

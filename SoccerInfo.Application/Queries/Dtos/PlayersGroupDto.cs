namespace SoccerInfo.Application.Queries.Dtos;

public class PlayersGroupDto
{
    public string DisplayName { get; set; } = null!;
    public IEnumerable<PlayerDto> Players { get; set; } = null!;
    public class PlayerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Position { get; set; } = null!;
        public int Age { get; set; }
        public float? MarketValue { get; set; }
        public string? MarketValueUnit { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? FaceImageBase64 { get; set; }
        public IEnumerable<string?>? NationalityImageBase64Collection { get; set; }
    }
}

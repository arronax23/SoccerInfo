namespace SoccerInfo.Extractor.Dto;

public class PlayersExtractionDto
{
    public List<TeamPlayersDto> Extraction{ get; set; } = new List<TeamPlayersDto> { };

    public class TeamPlayersDto
    {
        public string TeamName { get; set; } = null!;
        public List<PlayerDto> Players { get; set; } = new List<PlayerDto> { };
    }

    public class PlayerDto
    {
        public string Name { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string? FaceImageBase64 { get; set; } = null!;
        public List<string?> NationalityImageBase64Collection { get; set; } = new List<string?> { };
    }
}

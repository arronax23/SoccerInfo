namespace SoccerInfo.Extractor.Dto;

public class ExtractionDto
{
    public List<LeagueDto> Leagues { get; set; } = new List<LeagueDto>();

    public class LeagueDto
    {
        public string Name { get; set; } = null!;
        public string? LeagueImageBase64 { get; set; }
        public string Country { get; set; } = null!;
        public string? CountryFlagBase64 { get; set; }
        public List<TeamDto> Teams { get; set; } = new List<TeamDto>();
    }

    public class TeamDto
    {
        public string Name { get; set; } = null!;
        public string? TeamImageBase64 { get; set; }
        public List<PlayerDto> Players { get; set; } = new List<PlayerDto>();
    }

    public class PlayerDto
    {
        public string Name { get; set; } = null!;
        public int? Number { get; set; }
        public string Position { get; set; } = null!;
        public int Age { get; set; }
        public float? MarketValue { get; set; }
        public string? MarketValueUnit { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? FaceImageBase64 { get; set; }
        public List<string?> NationalityImageBase64Collection { get; set; } = new List<string?> { };
    }


}

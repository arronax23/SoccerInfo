namespace SoccerInfo.Extractor.Dto;

public class ExtractionData
{
    public List<LeagueData> Leagues { get; set; } = new List<LeagueData>();

    public class LeagueData
    {
        public string Name { get; set; } = null!;
        public string? LeagueImageBase64 { get; set; }
        public string Country { get; set; } = null!;
        public string? CountryFlagBase64 { get; set; }
        public List<TeamData> Teams { get; set; } = new List<TeamData>();
    }

    public class TeamData
    {
        public string Name { get; set; } = null!;
        public string? TeamImageBase64 { get; set; }
        public List<PlayerData> Players { get; set; } = new List<PlayerData>();
    }

    public class PlayerData
    {
        public string Name { get; set; } = null!;
        public int? Number { get; set; }
        public string Position { get; set; } = null!;
        public int Age { get; set; }
        public float? MarketValue { get; set; }
        public string? MarketValueUnit { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? FaceImageBase64 { get; set; }
        public List<NationalityData?> Nationalities { get; set; } = new List<NationalityData?>();
        public int TransfermarktId { get; set; }
        public string TransfermarktURL { get; set; } = null!;
    }

    public class NationalityData
    {
        public string Country { get; set; } = null!;
    }
}

using SoccerInfo.Persistence.Data.Models;

namespace SoccerInfo.Application.Queries.Dtos;

public class PlayerDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Position { get; set; } = null!;
    public int Age { get; set; }
    public float? MarketValue { get; set; }
    public string? MarketValueUnit { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? FaceImageBase64 { get; set; }
    public IEnumerable<NationalityDto> Nationalities { get; set; } = null!;
    public IEnumerable<MarketValueChangeDto>? MarketValueChanges { get; set; }

    public class NationalityDto
    {
        public string Country { get; set; } = null!;
        public string? CountryFlagBase64Image { get; set; }
    }

    public class MarketValueChangeDto
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public float? MarketValue { get; set; }
        public string? MarketValueUnit { get; set; }
        public DateTime ChangeDate { get; set; }
        public string? TeamImageBase64 { get; set; }
        public int PlayerId { get; set; }
    }
}

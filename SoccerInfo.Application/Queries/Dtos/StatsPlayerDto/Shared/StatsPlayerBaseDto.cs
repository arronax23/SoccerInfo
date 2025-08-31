namespace SoccerInfo.Application.Queries.Dtos.StatsPlayerDto.Shared;
public abstract class StatsPlayerBaseDto
{
    public int PlayerId { get; set; }
    public int Index { get; set; }
    public string Name { get; set; } = null!;
    public string? FaceImageBase64 { get; set; }
    public string? FaceImageMimeType { get; set; }
    public LeagueDto League { get; set; } = null!;
    public TeamDto Team { get; set; } = null!;
    public IEnumerable<NationalityDto> Nationalities { get; set; } = null!;
    public string Position { get; set; } = null!;

    public class LeagueDto
    {
        public string Name { get; set; } = null!;
        public int LeagueId { get; set; }
        public string? Logo { get; set; }
        public string? LogoMimeType { get; set; }
    }

    public class TeamDto
    {
        public string Name { get; set; } = null!;
        public int TeamId { get; set; }
        public string? Logo { get; set; }
        public string? LogoMimeType { get; set; }
    }

    public class NationalityDto
    {
        public string Name { get; set; } = null!;
        public string ImageBase64 { get; set; } = null!;
    }
}

namespace SoccerInfo.Application.Queries.Dtos;

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

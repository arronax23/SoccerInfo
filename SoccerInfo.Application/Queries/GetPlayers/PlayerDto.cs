namespace SoccerInfo.Application.Commands.ExtarctPlayers;

public class PlayerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Position { get; set; } = null!;
    public string? FaceImageBase64 { get; set; }
    public IEnumerable<string?>? NationalityImageBase64Collection { get; set; }

}

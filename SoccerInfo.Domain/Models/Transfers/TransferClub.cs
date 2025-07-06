using SoccerInfo.Domain.Models.Abstractions;

namespace SoccerInfo.Domain.Models.Transfers;
public class TransferClub : BaseEntity
{
    public string Name { get; set; } = null!;
    public int TransfermarktId { get; set; }
    public string? ClubImageBase64 { get; set; }
    public string? Country { get; set; }
    public string? CountryImageBase64 { get; set; }
}

using SoccerInfo.Domain.Models.Abstractions;
using static SoccerInfo.Domain.Models.Transfers.Transfer;

namespace SoccerInfo.Domain.Models.Transfers;
public class ClubOverview : BaseEntity
{
    public string Name { get; set; } = null!;
    public int TransfermarktId { get; set; }
    public string? TrasnfermarktUrl { get; set; } = null!;
    public string? ClubImageBase64 { get; set; }
    public string? Country { get; set; }
    public string? CountryImageBase64 { get; set; }
}

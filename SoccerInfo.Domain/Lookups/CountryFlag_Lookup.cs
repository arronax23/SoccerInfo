using SoccerInfo.Domain.Models;
using SoccerInfo.Domain.Models.Abstractions;

namespace SoccerInfo.Domain.Lookups;
public class CountryFlag_Lookup : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TwoLetterISOCode { get; set; } = string.Empty;
    public virtual Image Image { get; set; } = null!;
    public int ImageId { get; set; }
}

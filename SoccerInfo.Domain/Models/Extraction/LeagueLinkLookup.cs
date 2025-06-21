using SoccerInfo.Domain.Models.Abstractions;

namespace SoccerInfo.Domain.Models.Extraction;

public class LeagueLinkLookup : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Value { get; set; } = null!;
    public bool IsActive { get; set; }
}

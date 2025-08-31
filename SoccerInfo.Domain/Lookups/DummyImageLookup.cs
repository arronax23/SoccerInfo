using SoccerInfo.Domain.Models.Abstractions;

namespace SoccerInfo.Domain.Lookups;

public class DummyImageLookup : IEntity
{
    public int Id { get; set; }
    public EntityType Type { get; set; }
    public string TypeName { get; set; } = null!; 
    public string Base64 { get; set; } = null!;
    public string MimeType { get; set; } = null!;

    public enum EntityType
    {
        League,
        Team,
        Player
    }
}

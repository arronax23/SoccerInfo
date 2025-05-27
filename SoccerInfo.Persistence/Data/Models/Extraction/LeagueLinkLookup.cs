namespace SoccerInfo.Persistence.Data.Models.Extraction;

public class LeagueLinkLookup
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Value { get; set; } = null!;
    public bool IsActive { get; set; }
}

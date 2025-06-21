namespace SoccerInfo.Domain.Models.Abstractions;

public abstract class BaseEntity : IEntity, IAuditable
{
    public int Id { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
}

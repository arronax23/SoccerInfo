namespace SoccerInfo.Persistence.Data.Models.Abstractions;

public interface IAuditable
{
    DateTime? CreatedDate { get; set; }
    DateTime? LastUpdatedDate { get; set; }
}

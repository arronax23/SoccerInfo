using SoccerInfo.Persistence.Data.Models.Abstractions;

namespace SoccerInfo.Persistence.Data.Models.PlayerCharacteristicsAggregate;

public class PlayerCharacteristic : IEntity
{
    public int Id { get; set; }
    public BrithPlace? BrithPlace { get; set; }
    public NationalTeam? NationalTeam { get; set; }
    public string? LeadingFoot { get; set; }
    public float? Height { get; set; }
    public DateTime? ClubJoinDate { get; set; }
    public DateTime? ContractExpirationDate { get; set; }
    public virtual ICollection<SocialMedia>? Socials { get; set; }
    public virtual ICollection<OutfieldPlayerStats>? OutfieldPlayerStats { get; set; }
    public virtual ICollection<GoalKeeperStats>? GoalKeeperStats { get; set; }
    public int PlayerId { get; set; }

}

public class BrithPlace
{
    public string? City { get; set; }
    public string? Country { get; set; }
}

public class NationalTeam
{
    public string? Name { get; set; }
    public int Caps { get; set; }
    public int Goals { get; set; }
    public string? Country { get; set; }
}

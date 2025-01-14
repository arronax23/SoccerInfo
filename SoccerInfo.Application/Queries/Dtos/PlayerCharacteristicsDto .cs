namespace SoccerInfo.Application.Queries.Dtos;

public class PlayerCharacteristicsDto
{
    public int Id { get; set; }
    public bool IsGoalkeeper { get; set; }
    public BrithPlaceDto? BrithPlace { get; set; }
    public NationalTeamDto? NationalTeam { get; set; }
    public string? LeadingFoot { get; set; }
    public float? Height { get; set; }
    public DateTime? ClubJoinDate { get; set; }
    public DateTime? ContractExpirationDate { get; set; }
    public ICollection<SocialMediaDto>? Socials { get; set; }
    public ICollection<OutfieldPlayerStatsDto>? OutfieldPlayerStats { get; set; }
    public ICollection<GoalKeeperStatsDto>? GoalKeeperStats { get; set; }


    public class BrithPlaceDto
    {
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? CountryBase64Image { get; set; }
    }

    public class NationalTeamDto
    {
        public int Caps { get; set; }
        public int Goals { get; set; }
        public string? Country { get; set; }
        public string? CountryBase64Image { get; set; }
    }

    public class SocialMediaDto
    {
        public string? Platform { get; set; }
        public string? Link { get; set; }

    }

    public abstract class StatsBaseDto
    {
        public virtual StatsLeagueDto League { get; protected set; } = null!;
        public virtual int? MatchesPlayed { get; protected set; }
        public virtual int? MinutesPlayed { get; protected set; }

    }

    public class OutfieldPlayerStatsDto : StatsBaseDto
    {
        public int? Goals { get; private set; }
        public int? Assists { get; private set; }
        public int PlayerCharacteristicId { get; private set; }
    }

    public class GoalKeeperStatsDto : StatsBaseDto
    {
        public int? GoalsConceded { get; private set; }
        public int? CleanSheets { get; private set; }
        public int PlayerCharacteristicId { get; private set; }

    }

    public class StatsLeagueDto
    {
        public string? Name { get; set; }
        public string? Base64Image { get; set; }

    }

}



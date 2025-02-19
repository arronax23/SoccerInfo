namespace SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto;

public class PlayersCharacteristicsExtractionData
{
    public List<PlayerCharacteristicsData> PlayersCharacteristics { get; set; } 
        = new List<PlayerCharacteristicsData>();

    public class PlayerCharacteristicsData
    {
        public int TransfermarktId { get; set; }
        public BrithPlaceData? BrithPlace { get; set; }
        public NationalTeamData? NationalTeam { get; set; }
        public string? LeadingFoot { get; set; }
        public float? Height { get; set; }
        public DateTime? ClubJoinDate { get; set; }
        public DateTime? ContractExpirationDate { get; set; }
        public List<SocialMediaData> Socials { get; set; } = new List<SocialMediaData>();
        public bool IsGoalkeeper { get; set; }
        public List<OutfieldPlayerStatsData>? OutfieldPlayerStats { get; set; }
        public List<GoalKeeperStatsData>? GoalKeeperStats { get; set; } 

        public PlayerCharacteristicsData(int transfermarktId, bool isGoalkeeper)
        {
            TransfermarktId = transfermarktId;
            IsGoalkeeper = isGoalkeeper;

            if (IsGoalkeeper)
                GoalKeeperStats = new List<GoalKeeperStatsData>();
            else
                OutfieldPlayerStats = new List<OutfieldPlayerStatsData>();
        }
    }

    public abstract class StatsData
    {
        public virtual string? League { get; set; }
        public virtual string? LeagueBase64Image { get; set; }
        public virtual int? MatchesPlayed { get; set; }
        public virtual int? MinutesPlayed { get; set; }
    }

    public class GoalKeeperStatsData : StatsData
    {
        public int? GoalsConceded { get; set; }
        public int? CleanSheets { get; set; }

    }

    public class OutfieldPlayerStatsData : StatsData
    {
        public int? Goals { get; set; }
        public int? Assists { get; set; }
    }

    public class NationalTeamData
    {
        public string? Name { get; set; }
        public int Caps { get; set; }
        public int Goals { get; set; }
        public string? Country { get; set; }
    }

    public class BrithPlaceData
    {
        public string? City { get; set; }
        public string? Country { get; set; }
    }

    public class SocialMediaData
    {
        public string? Platform { get; set; }
        public string? Link { get; set; }
    }
}

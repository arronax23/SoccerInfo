using SoccerInfo.Persistence.Data.Models.Abstractions;

namespace SoccerInfo.Persistence.Data.Models.PlayerCharacteristicsAggregate;

public abstract class StatsBase : IEntity, IEquatable<StatsBase>
{
    public virtual int Id { get ; set; }
    public virtual StatsLeague League { get; protected set; } = null!;
    public virtual int? MatchesPlayed { get; protected set; }
    public virtual int? MinutesPlayed { get; protected set; }
    public virtual int LeagueId { get; set; }

    public virtual void UpdateLeague(StatsLeague? league)
    {
        if (league == null)
            throw new Exception("Updaing league failed, updating stats data is null");

        this.League = league;
    }

    protected virtual void UpdateBase(StatsBase? stats)
    {
        if (stats == null)
            throw new Exception("Updaing stats failed, updating stats data is null");

        this.League = stats.League;
        this.MatchesPlayed = stats.MatchesPlayed; 
        this.MinutesPlayed = stats.MinutesPlayed; 
    }

    public virtual bool Equals(StatsBase? other)
    {
        if (other == null)
            return false;
        else if (this.League.Equals(other.League))
            return true;
        else
            return false;
    }
}

public class OutfieldPlayerStats : StatsBase
{
    public int? Goals { get; private set; }
    public int? Assists { get; private set; }
    public int PlayerCharacteristicId { get; private set; }

    internal void Update(OutfieldPlayerStats? stats)
    {
        base.UpdateBase(stats);
        UpdateOutFieldPlayerStats(stats!);
    }

    private void UpdateOutFieldPlayerStats(OutfieldPlayerStats stats)
    {
        this.Goals = stats.Goals;
        this.Assists = stats.Assists;
    }
}

public class GoalKeeperStats : StatsBase
{
    public int? GoalsConceded { get; private set; }
    public int? CleanSheets { get; private set; }
    public int PlayerCharacteristicId { get; private set; }

    internal void Update(GoalKeeperStats? stats)
    {
        base.UpdateBase(stats);
        UpdateOutFieldPlayerStats(stats!);
    }

    private void UpdateOutFieldPlayerStats(GoalKeeperStats stats)
    {
        this.GoalsConceded = stats.GoalsConceded;
        this.CleanSheets = stats.CleanSheets;
    }
}

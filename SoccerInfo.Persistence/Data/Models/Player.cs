using SoccerInfo.Persistence.Data.Models.Abstractions;
using SoccerInfo.Persistence.Data.Models.PlayerCharacteristicsAggregate;
using SoccerInfo.Persistence.Data.Models.Stats;

namespace SoccerInfo.Persistence.Data.Models;

public partial class Player : IEntity, IEquatable<Player>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Position { get; set; } = null!;
    public int Age { get; set; }
    public float? MarketValue { get; set; }
    public string? MarketValueUnit { get; set; }
    public float? MarketValueNormalized { get; private set; }
    public DateTime DateOfBirth { get; set; }
    public string? FaceImageBase64 { get; set; }
    public int TransfermarktId { get; set; }
    public string TransfermarktURL { get; set; } = null!;
    public ICollection<Nationality> Nationalities { get; set; } = null!;
    public ICollection<MarketValueChange> MarketValueProgress { get; private set; } = null!;
    public PlayerCharacteristic? Characteristics { get;  internal set; }
    public PlayerStatistic Stats { get; set; } = null!;
    public Team Team { get; set; } = null!;
    public int TeamId { get; set; }

    public void AddNewMarketValueChanges(IEnumerable<MarketValueChange> marketValueChanges)
    {
        if (marketValueChanges.Any(x => x.PlayerTransferMarktId != this.TransfermarktId))
            throw new ArgumentException("TransfermarktId is not valid");

        if (this.MarketValueProgress.Count == 0)
        {
            this.MarketValueProgress = new HashSet<MarketValueChange>(marketValueChanges);
        }
        else
        {
            var newMarketValueChanges = marketValueChanges.Where(x => !this.MarketValueProgress.Any(y => y.Equals(x)));

            foreach (var newMarketValueChange in newMarketValueChanges)
                this.MarketValueProgress.Add(newMarketValueChange);
        }
    }

    public bool Equals(Player? other)
    {
        if (other == null) 
            return false;
        else if (this.Name == other.Name && this.DateOfBirth == other.DateOfBirth)
            return true;
        else
            return false;
    }
}

using Microsoft.EntityFrameworkCore;
using SoccerInfo.Persistence.Data.Models.Abstractions;
using SoccerInfo.Persistence.Data.Models.GeneralPosition;
using SoccerInfo.Persistence.Data.Models.PlayerCharacteristicsAggregate;
using SoccerInfo.Persistence.Data.Models.Stats;
using System.Linq.Expressions;

namespace SoccerInfo.Persistence.Data.Models;

public partial class Player : BaseEntity
{
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
    public virtual ICollection<Nationality> Nationalities { get; set; } = new List<Nationality>();
    public virtual ICollection<MarketValueChange> MarketValueProgress { get; private set; } = null!;
    public virtual PlayerCharacteristic? Characteristics { get;  internal set; }
    public virtual PlayerStatistic Stats { get; set; } = null!;
    public virtual Team Team { get; set; } = null!;
    public virtual GeneralPosition_Lookup GeneralPosition { get; set; } = null!;
    public int GeneralPositionId { get; set; }
    public int TeamId { get; set; }

    public void UpdateGeneralInfo(Player player)
    {
        this.Name = player.Name;
        this.Position = player.Position;
        this.Age = player.Age;
        this.MarketValue = player.MarketValue;
        this.MarketValueUnit = player.MarketValueUnit;
        this.DateOfBirth = player.DateOfBirth;
        this.FaceImageBase64 = player.FaceImageBase64;
        this.TransfermarktId = player.TransfermarktId;
        this.TransfermarktURL = player.TransfermarktURL;
    }

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
            var newMarketValueChanges = marketValueChanges.Where(x => !this.MarketValueProgress.Any(MarketValueChange.Matches(x)));

            foreach (var newMarketValueChange in newMarketValueChanges)
                this.MarketValueProgress.Add(newMarketValueChange);
        }
    }

    public static Expression<Func<Player, bool>> Matches(Player other) =>
        (Player p) => p.Name == other.Name && p.DateOfBirth == other.DateOfBirth;

}

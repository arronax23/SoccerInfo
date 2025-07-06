using SoccerInfo.Domain.Models.Abstractions;
using System.Linq.Expressions;

namespace SoccerInfo.Domain.Models.Transfers;

public enum TransferType
{
    Sold,
    Loaned,
    ReturnedFromLoan
}

public class Transfer : BaseEntity
{
    public int PlayerTransferMarktId { get; set; }
    public ClubInfo From { get; set; } = null!;
    public ClubInfo To { get; set; } = null!;
    public TransferType Type { get; set; }
    public DateTime Date { get; set; }
    public int Age { get; set; }
    public string Season { get; set; } = null!;
    public Money? Fee { get; set; }
    public int? FeeNormalized { get; set; }
    public Money? MarketValue{ get; set; }
    public int? MarketValueNormalized { get; set; }

    public static Func<Transfer, bool> Matches(Transfer other) =>
        (Transfer t) =>
            t.PlayerTransferMarktId == other.PlayerTransferMarktId &&
            t.Date == other.Date;

    public class ClubInfo
    {
        private ClubInfo()
        {
        }

        public static ClubInfo Create(int transfermarktId)
        {
            return new ClubInfo { ClubTransfermarktId = transfermarktId };
        
        }
        public void AssignTeam(Team team)
        {
            this.Team = Team;   
        }

        public void AssignClub(TransferClub transferClub)
        {
            this.Club = transferClub;
        }

        public int ClubTransfermarktId { get; private set; }
        public int? TeamId { get; private set; }
        public virtual Team? Team { get; private set; }
        public int? ClubId { get; private set; }
        public virtual TransferClub? Club { get; private set; }
    }

    public class Money
    {
        public Money(string? value, string? suffix)
        {
            this.Value = value;
            this.Suffix = suffix;   
        }

        public string? Value { get; set; }
        public string? Suffix { get; set; }
    }
}

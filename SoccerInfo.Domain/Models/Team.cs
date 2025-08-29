using SoccerInfo.Domain.Models.Abstractions;
using System.Diagnostics;
using System.Linq.Expressions;

namespace SoccerInfo.Domain.Models;

public class Team : BaseEntity
{
    public string Name { get; set; } = null!;
    public int TransfermarktId { get; set; }
    public string TransfermarktURL { get; set; } = null!;
    public Image? Logo { get; set; } = new Image();
    public int? LogoId { get; set; }
    public string? TeamImageBase64 { get; set; }
    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
    public int? LeagueId { get; set; }
    public virtual League? League { get; set; }

    public static Expression<Func<Team, bool>> Matches(Team other) => 
        (Team t) => t.Name == other.Name;


    public void Update(Team team)
    {
        this.Name = team.Name;
        this.Logo = team.Logo;
        this.TransfermarktId = team.TransfermarktId;
        this.TransfermarktURL = team.TransfermarktURL;
    }
}

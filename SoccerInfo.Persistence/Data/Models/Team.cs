using SoccerInfo.Persistence.Data.Models.Abstractions;
using System.Linq.Expressions;

namespace SoccerInfo.Persistence.Data.Models;

public class Team : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? TeamImageBase64 { get; set; }
    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
    public int? LeagueId { get; set; }
    public virtual League? League { get; set; }

    public static Expression<Func<Team, bool>> Matches(Team other) => 
        (Team t) => t.Name == other.Name;


    public void Update(Team team)
    {
        this.Name = team.Name;
        this.TeamImageBase64 = team.TeamImageBase64;
    }
}

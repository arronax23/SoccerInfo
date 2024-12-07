using SoccerInfo.Persistence.Data.Models;

namespace SoccerInfo.Application.Commands.UpdateTest;
internal class LeagueExtended : League
{
    public void PrepareTeams()
    {
        foreach (var team in Teams!)
        {
            team.League = this;
        }
    }
}

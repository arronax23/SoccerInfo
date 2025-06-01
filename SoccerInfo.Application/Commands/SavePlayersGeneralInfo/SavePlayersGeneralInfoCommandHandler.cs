using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.Data.Models;
using SoccerInfo.Persistence.Data.Models.Abstractions;
using SoccerInfo.Persistence.Data.Models.GeneralPosition;
using SoccerInfo.Persistence.EntityFrameworkExtensions;
using SoccerInfo.Shared.CQRS;
using System.Text.RegularExpressions;

namespace SoccerInfo.Application.Commands.SavePlayersGeneralInfo;

internal class SavePlayersGeneralInfoCommandHandler(
    IConfiguration configuration,
    ApplicationDbContext dbContext,
    IMapper mapper,
    GeneralPositionService generalPositionService) : ICommandHandler<SavePlayersGeneralInfoCommand>
{
    public async Task Handle(SavePlayersGeneralInfoCommand request, CancellationToken cancellationToken)
    {
        using var transaction = dbContext.Database.BeginTransaction();

        var extractedLeagues = mapper.Map<IEnumerable<League>>(request.Extraction.Leagues);

        foreach (var extractedLeague in extractedLeagues)
        {
            var dbLeague = dbContext.Leagues.SingleOrDefault(League.Matches(extractedLeague));
            League currentLeague = null!;

            if (dbLeague is not null)
            {
                dbLeague.Update(extractedLeague);
                currentLeague = dbLeague;
            }
            else
            {
                currentLeague = mapper.Map<League>(extractedLeague);
                dbContext.Leagues.Add(currentLeague);
            }

            foreach (var extractedTeam in extractedLeague.Teams)
            {
                var dbTeam = dbContext.Teams.SingleOrDefault(Team.Matches(extractedTeam));
                Team currentTeam = null!;

                if (dbTeam is not null)
                {
                    dbTeam.Update(extractedTeam);
                    currentTeam = dbTeam;
                }
                else
                {
                    currentTeam = mapper.Map<Team>(extractedTeam);
                    currentLeague.Teams.Add(currentTeam);
                }

                foreach (var extractedPlayer in extractedTeam.Players)
                {
                    var dbPlayer = dbContext.Players.SingleOrDefault(Player.Matches(extractedPlayer));
                    Player currentPlayer = null!;

                    if (dbPlayer is not null)
                    {
                        dbPlayer.Team = currentTeam;
                        dbPlayer.UpdateGeneralInfo(extractedPlayer);
                        currentPlayer = dbPlayer;
                    }
                    else
                    {
                        currentPlayer = mapper.Map<Player>(extractedPlayer);
                        currentTeam.Players.Add(currentPlayer);
                    }

                    generalPositionService.AttachGeneralPosition(currentPlayer);

                    foreach (var extractedNationality in extractedPlayer.Nationalities)
                    {
                        var dbNationality = dbContext.Nationalities.SingleOrDefault(Nationality.Matches(extractedNationality));

                        if (dbNationality is null)
                        {
                            currentPlayer.Nationalities.Add(extractedNationality);
                        }
                    }
                }
            }
        }

        dbContext.ChangeTracker.ShowEntries();


        await dbContext.SaveChangesAsync(cancellationToken);
        await transaction.ResolveAsync(configuration);
    }

    [Obsolete]
    private void UpdateOrAddEntity<TEntity>(TEntity extractedData) 
        where TEntity : class, IEntity, IEquatable<TEntity>
    {
        var dbEntity = dbContext.Set<TEntity>()
              .SingleOrDefault(extractedData.Equals);

        if (dbEntity is not null)
        {
        }
        else
        {
            var newLeague = mapper.Map<League>(extractedData);
            dbContext.Leagues.Add(newLeague);
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoccerInfo.Application.Interfaces;
using SoccerInfo.Domain.Models;
using SoccerInfo.Domain.Models.GeneralPosition;
using SoccerInfo.Domain.Repositories;
using SoccerInfo.Domain.Repositories.Generic;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.SavePlayersGeneralInfo;

internal class SavePlayersGeneralInfoCommandHandler(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IPlayerRepository playerRepository,
    IGenericRepository<League> leagueRepository,
    IGenericRepository<Team> teamRepository,
    IGenericRepository<Nationality> nationalityRepository,
    GeneralPositionService generalPositionService) : ICommandHandler<SavePlayersGeneralInfoCommand>
{
    public async Task Handle(SavePlayersGeneralInfoCommand request, CancellationToken cancellationToken)
    {
        using var transaction = unitOfWork.BeginTransaction();

        var extractedLeagues = mapper.Map<IEnumerable<League>>(request.Extraction.Leagues);

        foreach (var extractedLeague in extractedLeagues)
        {
            var dbLeague = await leagueRepository.SingleOrDefaultAsync(League.Matches(extractedLeague));
            League currentLeague = null!;

            if (dbLeague is not null)
            {
                dbLeague.Update(extractedLeague);
                currentLeague = dbLeague;
            }
            else
            {
                currentLeague = mapper.Map<League>(extractedLeague);
                await leagueRepository.AddAsync(currentLeague);
            }

            foreach (var extractedTeam in extractedLeague.Teams)
            {
                var dbTeam = await teamRepository.SingleOrDefaultAsync(Team.Matches(extractedTeam));
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
                    var dbPlayer = await playerRepository
                        .ToQuery()
                        .SingleOrDefaultAsync(p => p.TransfermarktId == extractedPlayer.TransfermarktId);
                    
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

                    await generalPositionService.AttachGeneralPosition(currentPlayer);

                    foreach (var extractedNationality in extractedPlayer.Nationalities)
                    {
                        var dbNationality = await nationalityRepository.SingleOrDefaultAsync(Nationality.Matches(extractedNationality));
                        var trackerNationality = GetNationalityFromChangeTracker(extractedNationality);

                        if (dbNationality is not null)
                        {
                            if(!currentPlayer.Nationalities.Contains(dbNationality))
                                currentPlayer.Nationalities.Add(dbNationality);
                        }
                        else if (trackerNationality is not null)
                        {
                            currentPlayer.Nationalities.Add(trackerNationality);
                        }
                        else
                        {
                            currentPlayer.Nationalities.Add(extractedNationality);
                        }
                    }
                }
            }
        }

        unitOfWork.ShowEntires();
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.ResolveTransactionAsync(transaction);
    }

    private Nationality? GetNationalityFromChangeTracker(Nationality extractedNationality)
    {
        return unitOfWork.GetEntires()
            .Where(entr => entr.Entity is Nationality)
            .Select(entr => (Nationality)entr.Entity)
            .SingleOrDefault(Nationality.Matches(extractedNationality).Compile());
    }
}
&
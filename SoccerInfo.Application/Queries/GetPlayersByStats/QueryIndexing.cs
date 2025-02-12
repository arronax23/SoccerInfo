using SoccerInfo.Application.Queries.Dtos.StatsPlayerDto.Shared;

namespace SoccerInfo.Application.Queries.GetPlayersByStats;
internal static class QueryIndexing
{
    public static IEnumerable<T> AddIndex<T>(this IEnumerable<T> statsPlayers, int pageNumber, int pageSize)
        where T : StatsPlayerBaseDto
    {
        for (int i = 0; i < statsPlayers.Count(); i++)
            statsPlayers.ElementAt(i).Index = (i + 1) + (pageNumber - 1) * pageSize;

        return statsPlayers;

        //return statsPlayers.Select((player, index) =>
        //{
        //    player.Index = index + 1;
        //    return player;
        //});
    }
}

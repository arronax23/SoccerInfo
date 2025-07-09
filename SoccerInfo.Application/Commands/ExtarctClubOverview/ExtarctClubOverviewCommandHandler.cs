using Microsoft.Extensions.Logging;
using SoccerInfo.BackendScraper.PlayersTransfers;
using SoccerInfo.Domain.Models.Transfers;
using SoccerInfo.Shared.CQRS;
using static SoccerInfo.BackendScraper.PlayersTransfers.ClubOverviewScraper;

namespace SoccerInfo.Application.Commands.ExtarctClubOverview;
internal class ExtarctClubOverviewCommandHandler(
    ClubOverviewScraper scraper
    ) : ICommandHandler<ExtarctClubOverviewCommand, IEnumerable<ClubOverview>>
{
    public async Task<IEnumerable<ClubOverview>> Handle(ExtarctClubOverviewCommand request, CancellationToken cancellationToken)
    {
        var extractedData = await scraper.Scrape(request.ClubsTransfermarktIds);
        return extractedData.Select(Map);
    }

    private ClubOverview Map(ClubOverviewData extractedData)
    {
        return new ClubOverview()
        {
            TransfermarktId = extractedData.TransfermarktId,
            TrasnfermarktUrl = extractedData.TransfermarktURL,
            Name = extractedData.Name,
            ClubImageBase64 = extractedData.Base64Image,
        };
    }
}

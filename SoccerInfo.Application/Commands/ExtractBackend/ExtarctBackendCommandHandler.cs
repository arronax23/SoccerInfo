using SoccerInfo.Extractor.Utilities;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Shared.CQRS;
using System.Net.Http.Json;

namespace SoccerInfo.Application.Commands.ExtarctBackend;

internal class ExtarctBackendCommandHandler(
    ApplicationDbContext dbContext,
    PuppeteerManager puppeteerManager,
    IHttpClientFactory httpClientFactory)
    : ICommandHandler<ExtarctBackendCommand>
{
    public async Task Handle(ExtarctBackendCommand request, CancellationToken cancellationToken)
    {
        using (var client = httpClientFactory.CreateClient())
        {
            var response = await client.GetAsync("https://www.transfermarkt.pl/ceapi/marketValueDevelopment/graph/247652");

            var a = await client.GetFromJsonAsync<ContentModel>("https://www.transfermarkt.pl/ceapi/marketValueDevelopment/graph/247652");

        }
    }

    public class ContentModel
    {
        public IEnumerable<ListModel> List { get; set; }
        public class ListModel
        {
            //public long X { get; set; }
            //public long Y { get; set; }
            public string Mw { get; set; }
            public string Datum_mw { get; set; }
            public string Verein { get; set; }
            public int Age { get; set; }
            public string Wappen { get; set; }

            //"x": 1425423600000,
            //"y": 100000,
            //"mw": "100 tys. €",
            //"datum_mw": "4 mar 2015",
            //"verein": "Maccabi Haifa",
            //"age": "19",
            //"wappen": "https://tmssl.akamaized.net//images/wappen/profil/1064_1626682431.png?lm=1626682431"

        }

    }
}

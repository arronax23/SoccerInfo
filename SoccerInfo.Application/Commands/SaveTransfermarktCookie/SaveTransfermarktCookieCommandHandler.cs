using SoccerInfo.FrontendScraper.AcceptCookies;
using SoccerInfo.FrontendScraper.Utilities;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.SaveTransfermarktCookie;
internal class SaveTransfermarktCookieCommandHandler(
    CookiesExtractor cookiesExtractor) : ICommandHandler<SaveTransfermarktCookieCommand>
{
    public async Task Handle(SaveTransfermarktCookieCommand request, CancellationToken cancellationToken)
        => await cookiesExtractor.Extract();
}

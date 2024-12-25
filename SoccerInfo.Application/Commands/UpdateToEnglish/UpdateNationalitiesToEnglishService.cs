using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.EntityFrameworkCore;
using SoccerInfo.FrontendScraper.Utilities;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.Data.Models;
using System.Text.RegularExpressions;

namespace SoccerInfo.Application.Commands.UpdateToEnglish;

public class UpdateNationalitiesToEnglishService(
    ApplicationDbContext dbContext,
    PuppeteerManager puppeteerManager)
{
    private readonly IList<Task> _extractionTasks = new List<Task>();
    private bool _isBrowserSearchStart = true;

    public async Task Update(List<Nationality> extractedNationalities)
    {
        var dbNationalities = dbContext.Nationalities.Include(x => x.CountryFlag).AsNoTracking();
        var dbCountryFlags = dbContext.CountryFlags_Lookup.AsNoTracking();

        var extractedNotFoundNationalities = UpdateNationalitiesWithMatchingNames(extractedNationalities, dbNationalities);

        await SearchAndAssign(extractedNotFoundNationalities, dbNationalities, dbCountryFlags);

        var a = extractedNationalities.Where(x => x.CountryFlagId != null).Count();

        await Console.Out.WriteLineAsync();

    }


    private List<Nationality> UpdateNationalitiesWithMatchingNames(List<Nationality> extractedNationalities, IQueryable<Nationality> dbNationalities)
    {
        List<Nationality> extractedNotFoundNationalities = new List<Nationality>();

        for (int i = 0; i < extractedNationalities.Count; i++)
        {
            var dbNationality = dbNationalities
                .SingleOrDefault(x => x.Country == extractedNationalities[i].Country || x.Country_Lookup == extractedNationalities[i].Country);

            if (dbNationality != null)
            {
                extractedNationalities[i].Id = dbNationality.Id;
                extractedNationalities[i].Country_Lookup = dbNationality.Country_Lookup;
                extractedNationalities[i].CountryFlagId = dbNationality.CountryFlagId;
                dbContext.Entry(extractedNationalities[i]).State = EntityState.Unchanged;

            }
            else
                extractedNotFoundNationalities.Add(extractedNationalities[i]);
        }

        return extractedNotFoundNationalities;

    }

    private async Task SearchAndAssign(List<Nationality> extractedNotFoundNationalities, IQueryable<Nationality> dbNationalities, IQueryable<CountryFlag_Lookup> dbCountryFlags)
    {
        await puppeteerManager.LaunchBrowser(headless: true);

        bool isStart = true;

        for (int i = 0; i < extractedNotFoundNationalities.Count(); i++)
        {

            if (extractedNotFoundNationalities[i].Country == "England" ||
                extractedNotFoundNationalities[i].Country == "Scotland" ||
                extractedNotFoundNationalities[i].Country == "Northern Ireland" ||
                extractedNotFoundNationalities[i].Country == "Wales" ||
                extractedNotFoundNationalities[i].Country == "Kosovo"
                )
            {
                var dbNationality = dbNationalities.SingleOrDefault(x => x.Country == extractedNotFoundNationalities[i].Country);
                if (dbNationality != null)
                {
                    extractedNotFoundNationalities[i].Id = dbNationality.Id;
                    extractedNotFoundNationalities[i].Country_Lookup = dbNationality.Country_Lookup;
                    extractedNotFoundNationalities[i].CountryFlagId = dbNationality.CountryFlagId;

                    dbContext.Entry(extractedNotFoundNationalities[i]).State = EntityState.Unchanged;
                    continue;
                }
                else
                {
                    var dbCountryFlag = dbCountryFlags.SingleOrDefault(x => x.Name == extractedNotFoundNationalities[i].Country);

                    if (dbCountryFlag != null)
                    {
                        extractedNotFoundNationalities[i].CountryFlagId = dbCountryFlag.Id;
                        extractedNotFoundNationalities[i].Country_Lookup = dbCountryFlag.Name;
                        dbContext.Entry(extractedNotFoundNationalities[i]).State = EntityState.Added;
                        continue;
                    }
                    else
                    {
                        throw new NotMatchedCountryException($"Country not matched exception Country: {extractedNotFoundNationalities[i].Country}");
                    }
                }

            }

            _extractionTasks.Add(SearchInBrowser(extractedNotFoundNationalities, dbNationalities, dbCountryFlags, i));

            await Task.WhenAll(_extractionTasks);

        }



        await puppeteerManager.CloseBrowser();
    }

    private async Task SearchInBrowser(List<Nationality> extractedNotFoundNationalities, IQueryable<Nationality> dbNationalities, IQueryable<CountryFlag_Lookup> dbCountryFlags, int i)
    {

        var page = await puppeteerManager.Browser.NewPageAsync();
        await page.GoToAsync(@"https://www.google.pl/", PuppeteerSharp.WaitUntilNavigation.Networkidle0);

        if (_isBrowserSearchStart)
        {
            await page.ClickAsync("#L2AGLb");
            _isBrowserSearchStart = false;
        }

        Again:
        try
        {
            await page.WaitForNetworkIdleAsync(new PuppeteerSharp.WaitForNetworkIdleOptions()
            {
                Timeout = 3000
            });


            await page.TypeAsync("textarea[aria-label=Szukaj]", $"{extractedNotFoundNationalities[i].Country} iso 3166-2");
            await page.Keyboard.PressAsync("Enter");
            await page.WaitForNavigationAsync();
        }
        catch (Exception)
        {
            goto Again;
        }

        var node = await page.CreateHtmlNodeFromPage();
        var headers = node.QuerySelectorAll("h3");
        string countryCode = string.Empty;
        string pattern = @"ISO 3166-2:(\w{2})";

        for (int j = 0; j < headers.Count; j++)
        {
            Match match = Regex.Match(headers[j].InnerText, pattern);

            if (match.Success)
            {
                countryCode = match.Groups[1].Value;
                break;
            }
        }

        await Console.Out.WriteLineAsync(extractedNotFoundNationalities[i].Country);
        await Console.Out.WriteLineAsync(countryCode);

        try
        {
            var dbNationality = dbNationalities.SingleOrDefault(x => x.CountryFlag!.TwoLetterISOCode == countryCode);

            if (dbNationality != null)
            {
                extractedNotFoundNationalities[i].Id = dbNationality.Id;
                extractedNotFoundNationalities[i].Country_Lookup = dbNationality.Country_Lookup;
                extractedNotFoundNationalities[i].CountryFlagId = dbNationality.CountryFlagId;

                dbContext.Entry(extractedNotFoundNationalities[i]).State = EntityState.Unchanged;
            }
            else
            {
                var dbCountryFlag = dbCountryFlags.SingleOrDefault(x => x.TwoLetterISOCode == countryCode);

                if (dbCountryFlag != null)
                {
                    extractedNotFoundNationalities[i].CountryFlagId = dbCountryFlag.Id;
                    extractedNotFoundNationalities[i].Country_Lookup = dbCountryFlag.Name;
                    dbContext.Entry(extractedNotFoundNationalities[i]).State = EntityState.Added;
                }
                else
                {
                    throw new NotMatchedCountryException($"Country not matched exception code: {countryCode}");
                }

            }
        }
        catch (Exception)
        {
            await puppeteerManager.CloseBrowser();
            throw;
        }
    }
}

public class NotMatchedCountryException : Exception
{
    public NotMatchedCountryException(string message) : base(message)
    {

    }
}



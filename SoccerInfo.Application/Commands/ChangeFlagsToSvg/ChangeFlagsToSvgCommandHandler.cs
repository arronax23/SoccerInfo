using Microsoft.Extensions.Logging;
using Nager.Country;
using Serilog;
using SoccerInfo.Application.Interfaces;
using SoccerInfo.Domain.Models;
using SoccerInfo.Domain.Repositories.Generic;
using SoccerInfo.Shared.CQRS;
using System.Globalization;
using System.Reflection;

namespace SoccerInfo.Application.Commands.ChangeFlagsToSvg;

internal class ChangeFlagsToSvgCommandHandler(
    ILogger<ChangeFlagsToSvgCommandHandler> logger,
    IGenericRepository<Nationality> nationalityRepository,
    IUnitOfWork unitOfWork
    ) : ICommandHandler<ChangeFlagsToSvgCommand>
{
    private readonly string[] extractCountriesCodes = ["ENG", "NIR", "SCT", "WLS", "XK"];

    public async Task Handle(ChangeFlagsToSvgCommand request, CancellationToken cancellationToken)
    {
        //var countryNamesAndCodes = Some2();
        //var nagerList = NagerTesting();

        //var countryImgAndCode = ReadFiles();
        //var d = CombineData(nagerList, countryImgAndCode);
        //dbContext.CountryFlags_Lookup.AddRange(d);
        //dbContext.SaveChanges();

        //Translate(dbContext.Nationalities, dbContext.CountryFlags_Lookup.ToList());
        //dbContext.SaveChanges();

        //var nationalities = dbContext.Nationalities.Where(x => x.CountryFlagId == null).ToList();


        //await Console.Out.WriteLineAsync();
        //dbContext.SaveChanges();


        var files = ReadFiles();

        var nationalities = nationalityRepository.ToQuery().Where(x => x.CountryFlagId == null);

        var ENG = nationalities.Single(x => x.Country == "England");
        var SCT = nationalities.Single(x => x.Country == "Scotland");
        var WLS = nationalities.Single(x => x.Country == "Wales");
        var NIR = nationalities.Single(x => x.Country == "Northern Ireland");
        var XK = nationalities.Single(x => x.Country == "Kosovo");

        ENG.CountryFlag = new CountryFlag_Lookup()
        {
            Name = ENG.Country,
            ImageSvgBase64 = files.Single(x => x.Item2 == nameof(ENG)).Item1,
            TwoLetterISOCode = files.Single(x => x.Item2 == nameof(ENG)).Item2
        };

        SCT.CountryFlag = new CountryFlag_Lookup()
        {
            Name = SCT.Country,
            ImageSvgBase64 = files.Single(x => x.Item2 == nameof(SCT)).Item1,
            TwoLetterISOCode = files.Single(x => x.Item2 == nameof(SCT)).Item2
        };

        WLS.CountryFlag = new CountryFlag_Lookup()
        {
            Name = WLS.Country,
            ImageSvgBase64 = files.Single(x => x.Item2 == nameof(WLS)).Item1,
            TwoLetterISOCode = files.Single(x => x.Item2 == nameof(WLS)).Item2
        };

        NIR.CountryFlag = new CountryFlag_Lookup()
        {
            Name = NIR.Country,
            ImageSvgBase64 = files.Single(x => x.Item2 == nameof(NIR)).Item1,
            TwoLetterISOCode = files.Single(x => x.Item2 == nameof(NIR)).Item2
        };

        XK.CountryFlag = new CountryFlag_Lookup()
        {
            Name = XK.Country,
            ImageSvgBase64 = files.Single(x => x.Item2 == nameof(XK)).Item1,
            TwoLetterISOCode = files.Single(x => x.Item2 == nameof(XK)).Item2
        };


        unitOfWork.SaveChanges();

    }


    private List<(string, string)> Some2()
    {
        List<(string, string)> countryNamesAndCodes = new List<(string, string)>();
        foreach (var culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
        {
            RegionInfo region = new RegionInfo(culture.Name);
            if (!countryNamesAndCodes.Select(x => x.Item2).Contains(region.TwoLetterISORegionName))
            {
                countryNamesAndCodes.Add((region.EnglishName, region.TwoLetterISORegionName));
            }
        }
        var sorted = countryNamesAndCodes.OrderBy(x => x.Item1).ToList();// Optional: Sort alphabetically

        return sorted;
    }

    private List<(string, string)> ReadFiles()
    {
        var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),"flags");
        var txtFiles = Directory.EnumerateFiles(path, "*.svg");

        List<(string, string)> countryImgAndCode = new List<(string, string)>();

        foreach (var filePath in txtFiles)
        {
            string svgContent = File.ReadAllText(filePath);
            string base64Data = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(svgContent));

            string code = filePath.Split('\\').Last().Split('.').First().ToUpper().Trim();

            countryImgAndCode.Add((base64Data, code));
        }

        countryImgAndCode = countryImgAndCode.Where(x => extractCountriesCodes.Contains(x.Item2)).ToList();

        return countryImgAndCode;
    }


    static List<(string, string)> NagerTesting()
    {
        List<(string, string)> nagerList = new List<(string, string)>();
        var countryProvider = new CountryProvider();
        var countries = countryProvider.GetCountries();
        foreach (var country in countries)
        {
            Log.Logger.Information($"{country.Alpha2Code} - {country.CommonName} - {country.OfficialName} - {country.NativeName}");
            nagerList.Add((country.CommonName, country.Alpha2Code.ToString()));
        }





        Log.Logger.Information(countries.Count().ToString());
        return nagerList;
    }

    private void Translate(IEnumerable<Nationality> nationalities, List<CountryFlag_Lookup> countryFlags)
    {
        //List<(string, string, string)> list = new();
        //var translationProvider = new TranslationProvider();
        //var nagerList = NagerTesting();

        //foreach (var nager in nagerList)
        //{
        //    list.Add((
        //        nager.Item1,
        //        nager.Item2,
        //        translationProvider.GetCountryTranslatedName(nager.Item2, LanguageCode.PL)
        //    ));
        //}

        foreach (var nat in nationalities)
        {
           // var result = list.SingleOrDefault(x => x.Item1 == nat.Country);

           // if (result.Item1 == null && result.Item2 == null && result.Item3 == null)
           //     Console.WriteLine($"NO Match: {nat.Country}");
           //else
           //{
           //     nat.Country = result.Item1;
           //}

            var countryflag = countryFlags.SingleOrDefault(x => x.Name == nat.Country);
            if (countryflag != null)
            {
                nat.CountryFlagId = countryflag.Id;
            }

        }
    }


    private List<CountryFlag_Lookup> CombineData(List<(string, string)> nagerList, List<(string, string)> countryImgAndCode)
    {
        List<CountryFlag_Lookup> countryFlag_Lookups = new();

        foreach (var cimac in countryImgAndCode)
        {
            try
            {
                Console.WriteLine(cimac.Item2);
                var nager = nagerList.Single(x => x.Item2 == cimac.Item2);
                countryFlag_Lookups.Add(new CountryFlag_Lookup()
                {
                    Name = nager.Item1,
                    TwoLetterISOCode = nager.Item2,
                    ImageSvgBase64 = cimac.Item1
                });

                Console.WriteLine(cimac.Item2);
            }
            catch (Exception)
            {
                Console.WriteLine();
            }


        }

        
        return countryFlag_Lookups.OrderBy(x => x.Name).ToList();
    }

}

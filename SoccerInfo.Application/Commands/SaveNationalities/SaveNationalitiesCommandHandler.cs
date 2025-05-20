using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Nager.Country;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.Data.Models;
using SoccerInfo.Shared.CQRS;
using System.Globalization;

namespace SoccerInfo.Application.Commands.SaveNationalities;

internal class SaveNationalitiesCommandHandler(ApplicationDbContext dbContext) : ICommandHandler<SaveNationalitiesCommand>
{
    public async Task Handle(SaveNationalitiesCommand request, CancellationToken cancellationToken)
    {
        using var reader = new StreamReader("nationalities_prepared.csv");
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            HasHeaderRecord = false,
            MissingFieldFound = null
        };
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<Country>();

        records = records.Select(x => new Country()
        {
            Name = x.Name,
            IsoCode = x.IsoCode.Trim(),
            CommonName = x.CommonName
        });



        records = AddCommonName(records);
        records = FilterOutExisingNationalities(records).ToList();

        
        foreach (var country in records)
        {
            Console.WriteLine($"({country.Name}) ({country.IsoCode}): ({country.CommonName})");
            File.AppendAllText("nat.txt", $"({country.Name}) ({country.IsoCode}): ({country.CommonName})"+ Environment.NewLine);
        }


        dbContext.Nationalities.AddRange(records.Select(x => new Nationality()
        {
            Country = x.Name,
            Country_Lookup = x.CommonName,
            CountryFlagId = dbContext.CountryFlags_Lookup.AsNoTracking().Single(y => y.Name == x.CommonName).Id,
        }));

        dbContext.SaveChanges();

    }

    public IEnumerable<Country> FilterOutExisingNationalities(IEnumerable<Country> records)
    {
        var nationalities = dbContext.Nationalities.AsNoTracking();

        return records.Where(x => !nationalities.Any(n => n.Country == x.Name));
    }


    public IEnumerable<Country> AddCommonName(IEnumerable<Country> records)
    {
        var countryProvider = new CountryProvider();
        var countries = countryProvider.GetCountries();

        return records.Select(x => new Country()
        {
            Name = x.Name,
            IsoCode = x.IsoCode.Trim(),
            CommonName = countries.SingleOrDefault(c => c.Alpha2Code.ToString() == x.IsoCode)?.CommonName
        });
    }

   public class Country
    {
        public string Name { get; set; } = null!;
        public string IsoCode { get; set; } = null!;
        public string CommonName { get; set; }
    }
}

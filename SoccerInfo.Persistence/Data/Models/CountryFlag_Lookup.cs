namespace SoccerInfo.Persistence.Data.Models;
public class CountryFlag_Lookup
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TwoLetterISOCode { get; set; } = string.Empty;
    public string ImageSvgBase64 { get; set; } = string.Empty;
}

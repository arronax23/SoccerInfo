using SoccerInfo.Domain.Models.Abstractions;

namespace SoccerInfo.Domain.Models.Extraction;

public class ExtractionInfo : BaseEntity
{
    public Guid Guid { get; private set; }
    public ExtractionType Type { get; private set; }
    public string TypeName { get; private set; } = null!;
    public DateTime Start { get; private set; }
    public DateTime End { get; private set; }
    public string? Duration { get; private set; }
    public bool IsFinished { get; private set; }
    public string? JsonDataFileName { get; private set; }

    protected ExtractionInfo()
    {
    }

    public static ExtractionInfo CreateAndStart(ExtractionType type)
    {
        return new ExtractionInfo()
        {
            Guid = Guid.NewGuid(),
            Start = DateTime.Now,
            Type = type,
            TypeName = type.ToString(),
            IsFinished = false
        };
    }

    public void Finish(string jsonDataFileName)
    {
        End = DateTime.Now;
        Duration = PrepareDurationText();
        IsFinished = true;
        JsonDataFileName = jsonDataFileName;
    }

    public enum ExtractionType
    {
        PlayersGeneralInfo,
        PlayersCharacteristics,
        MarketValueProgress,
        TransferHistory
    }


    private string PrepareDurationText()
    {
        TimeSpan span = (End - Start).Duration();

        string daysText = span.Days != 0 ? $"{span.Days} Days " : string.Empty;
        string hoursText = span.Hours != 0 ? $"{span.Hours} Hours " : string.Empty;
        string minutesText = span.Minutes != 0 ? $"{span.Minutes} Minutes " : string.Empty;
        string secondsText = span.Seconds != 0 ? $"{span.Seconds} Seconds " : string.Empty;

        return $"{daysText}{hoursText}{minutesText}{secondsText}";
    }
}

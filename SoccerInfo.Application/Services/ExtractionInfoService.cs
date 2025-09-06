using SoccerInfo.Application.Abstractions.Interfaces;
using SoccerInfo.Domain.Models.Extraction;
using SoccerInfo.Domain.Repositories.Generic;
namespace SoccerInfo.Application.Services;
public class ExtractionInfoService(
    IGenericRepository<ExtractionInfo> extractionInfoRepository,
    IJsonFileDataManager jsonFileDataManager,
    IUnitOfWork unitOfWork)

{
    public async Task<TData> Use<TData>(Func<Task<TData>> extraction, ExtractionInfo.ExtractionType type, bool saveToFile)
    {
        var infoGuid = await StartExtractionInfo(type);
        var extractionData = await extraction();

        var fullFileName = string.Empty;

        if (saveToFile && extractionData is not null)
            fullFileName = await jsonFileDataManager.SaveData(extractionData, GetExtractionFileNameByType(type));

        await FinishExtractionInfo(infoGuid, fullFileName);

        return extractionData;
    }


    private string GetExtractionFileNameByType(ExtractionInfo.ExtractionType type)
    {
        return type switch
        {
            ExtractionInfo.ExtractionType.PlayersGeneralInfo => "players_general_info_data",
            ExtractionInfo.ExtractionType.PlayersCharacteristics => "characteristics_data",
            ExtractionInfo.ExtractionType.MarketValueProgress => "market_value_data",
            ExtractionInfo.ExtractionType.TransferHistory => "transfer_history_data",
            _ => throw new Exception("Not valid ExtractionType")
        };
    }


    private async Task<Guid> StartExtractionInfo(ExtractionInfo.ExtractionType type)
    {
        var extractionInfo = ExtractionInfo.CreateAndStart(type);
        await extractionInfoRepository.AddAsync(extractionInfo);
        await unitOfWork.SaveChangesAsync();

        return extractionInfo.Guid;
    }

    public async Task FinishExtractionInfo(Guid infoGuid, string jsonDataFileName)
    {
        var extractionInfo = extractionInfoRepository.ToQuery().Single(e => e.Guid == infoGuid);
        extractionInfo.Finish(jsonDataFileName);
        await unitOfWork.SaveChangesAsync();
    }
}

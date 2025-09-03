using SoccerInfo.Application.Abstractions.Interfaces;
using SoccerInfo.Domain.Models.Extraction;
using SoccerInfo.Domain.Repositories.Generic;
namespace SoccerInfo.Application.Services;
public class ExtractionInfoService(
    IGenericRepository<ExtractionInfo> extractionInfoRepository,
    IUnitOfWork unitOfWork
    )
{
    public async Task<Guid> CreateExtractionInfo(ExtractionInfo.ExtractionType type)
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

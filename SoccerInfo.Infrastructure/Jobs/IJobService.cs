using Quartz;

namespace SoccerInfo.Infrastructure.Jobs;

public interface IJobService : IJob
{
    static abstract string Name { get; }
    static abstract JobKey Key { get; }
}

using Serilog;

namespace SoccerInfo.Shared.Utilities;

public static class Benchmark
{
    public static void ExecuteAndMeasureTime(Action action, string methodName)
    {
        var startDate = DateTime.Now;
        action();
        Log.Logger.Information($"Execution time for {methodName}: {(DateTime.Now - startDate).TotalSeconds}");
    }

    public static double ExecuteAndGetTime(Action action, string methodName)
    {
        var startDate = DateTime.Now;
        action();
        Log.Logger.Information($"Execution time for {methodName}: {(DateTime.Now - startDate).TotalSeconds}");
        return (DateTime.Now - startDate).TotalSeconds;
    }

    public static T ExecuteAndMeasureTime<T>(Func<T> func, string methodName)
    {
        var startDate = DateTime.Now;
        var result = func();
        Log.Logger.Information($"Execution time for {methodName}: {(DateTime.Now - startDate).TotalSeconds}");
        return result;
    }

    public static async Task<T> ExecuteAndMeasureTimeAsync<T>(Func<Task<T>> func, string methodName)
    {
        var startDate = DateTime.Now;
        var result = await func();
        Log.Logger.Information($"Execution time for {methodName}: {(DateTime.Now - startDate).TotalSeconds}");
        return result;
    }

    public static async Task ExecuteAndMeasureTimeAsync(Func<Task> func, string methodName)
    {
        var startDate = DateTime.Now;
        await func();
        Log.Logger.Information($"Execution time for {methodName}: {(DateTime.Now - startDate).TotalSeconds}");
    }



}

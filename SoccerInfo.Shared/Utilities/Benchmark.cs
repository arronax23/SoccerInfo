
namespace SoccerInfo.Shared.Utilities;
public static class Benchmark
{
    //public static async Task ExecuteAndMeasureTimeAsync(Func<Task> action, string methodName)
    //{
    //    var startDate = DateTime.Now;
    //    await action();
    //    Log.Logger.Information($"Execution time for {methodName}: {(DateTime.Now - startDate).TotalSeconds}");
    //}

    //public static async Task<T> ExecuteAndMeasureTimeAsync<T>(Func<Task<T>> func, string methodName)
    //{
    //    var startDate = DateTime.Now;
    //    var result = await func();
    //    Log.Logger.Information($"Execution time for {methodName}: {(DateTime.Now - startDate).TotalSeconds}");
    //    return result;
    //}

    public static void ExecuteAndMeasureTime(Action action, string methodName)
    {
        var startDate = DateTime.Now;
        action();
        Console.WriteLine($"Execution time for {methodName}: {(DateTime.Now - startDate).TotalSeconds}");
    }

    public static T ExecuteAndMeasureTime<T>(Func<T> func, string methodName)
    {
        var startDate = DateTime.Now;
        var result = func();
        Console.WriteLine($"Execution time for {methodName}: {(DateTime.Now - startDate).TotalSeconds}");
        return result;
    }
}

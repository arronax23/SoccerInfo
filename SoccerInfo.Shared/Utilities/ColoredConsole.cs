namespace SoccerInfo.Shared.Utilities;
public static class ColoredConsole
{
    public async static Task PrintColoredAsync(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        await Console.Out.WriteLineAsync(text);
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void PrintColored(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.White;
    }
}

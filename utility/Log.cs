namespace GameEngine.utility;

/// <summary>
/// Класс лога. Очень просто использовать.
/// </summary>
public class Log
{
    private static void Write(string prefix, string message, ConsoleColor color)
    {
        var previousColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine($"{prefix}: {message}");
        Console.ForegroundColor = previousColor;
    }

    public static void Normal(string message) => Write("[Message]", message, ConsoleColor.Green);
    public static void Info(string message) => Write("[Info]", message, ConsoleColor.Cyan);
    public static void Warning(string message) => Write("[Warning]", message, ConsoleColor.Yellow);
    public static void Error(string message) => Write("[Error]", message, ConsoleColor.Red);
    public static void Event(string message) => Write("[Event]", message, ConsoleColor.Blue);
}
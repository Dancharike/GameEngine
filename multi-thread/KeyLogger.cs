using System.Collections.Concurrent;

namespace GameEngine.multi_thread;

public class KeyLogger
{
    private static readonly ConcurrentQueue<string> _logQueue = new();
    private static readonly string _logFilePath = "log.txt";

    public static void Log(Keys key)
    {
        string entry = $"{DateTime.Now:HH:mm:ss.fff} - {key}";
        _logQueue.Enqueue(entry);
    }

    public static void Flush()
    {
        if(_logQueue.IsEmpty) {return;}
        
        using StreamWriter writer = new(_logFilePath, append: true);
        while (_logQueue.TryDequeue(out var entry))
        {
            writer.WriteLine(entry);
        }
    }
}
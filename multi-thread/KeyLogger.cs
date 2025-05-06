using System.Collections.Concurrent;

namespace GameEngine.multi_thread;

/// <summary>
/// Логгер, который записывает нажатые клавиши в файл.
/// Поддерживает потокобезопасную очередь и безопасную запись.
/// </summary>
public static class KeyLogger
{
    private static readonly ConcurrentQueue<string> _logQueue = new();
    private static readonly string _logFilePath = "log.txt";
    private static readonly object _fileLock = new();

    /// <summary>
    /// Добавляет запись о нажатой клавише в очередь.
    /// </summary>
    public static void Log(Keys key)
    {
        string entry = $"{DateTime.Now:HH:mm:ss.fff} - {key}";
        _logQueue.Enqueue(entry);
    }

    /// <summary>
    /// Записывает все накопленные события из очереди в лог-файл.
    /// Безопасен при параллельном доступе.
    /// </summary>
    public static void Flush()
    {
        if (_logQueue.IsEmpty) return;

        lock (_fileLock)
        {
            try
            {
                using StreamWriter writer = new(_logFilePath, append: true);

                while (_logQueue.TryDequeue(out var entry))
                {
                    writer.WriteLine(entry);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"[KeyLogger] Writing to log failure: {e.Message}");
            }
        }
    }
}
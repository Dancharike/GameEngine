using System.Text;

namespace GameEngine.multi_thread;

/// <summary>
/// Класс для периодического анализа активности игрока по лог-файлу.
/// Считывает log.txt, подсчитывает количество нажатий каждой клавиши и сохраняет отчёт.
/// Работает в отдельном потоке, чтобы не блокировать игровой цикл.
/// </summary>
public class ActivityAnalyzer
{
    private readonly string _logPath;
    private readonly string _reportPath;
    private bool _running = true;

    /// <param name="logPath">Путь к лог-файлу с нажатыми клавишами</param>
    /// <param name="reportPath">Путь к выходному файлу-отчёту</param>
    public ActivityAnalyzer(string logPath = "log.txt", string reportPath = "report.txt")
    {
        _logPath = logPath;
        _reportPath = reportPath;
    }

    /// <summary>
    /// Запускает анализатор в отдельном фоновом потоке.
    /// </summary>
    public void Start()
    {
        Thread thread = new(() =>
        {
            while (_running)
            {
                Thread.Sleep(10000); // 10 секунд
                try
                {
                    // открыть лог-файл на чтение, разрешая одновременную запись из другого потока
                    using var stream = new FileStream(_logPath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                    using var reader = new StreamReader(stream, Encoding.UTF8);

                    var keyCounts = new Dictionary<string, int>();
                    string? line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(" - "))
                        {
                            var parts = line.Split(" - ");
                            if (parts.Length == 2)
                            {
                                string key = parts[1];
                                if (!keyCounts.ContainsKey(key))
                                {
                                    keyCounts[key] = 0;
                                }
                                keyCounts[key]++;
                            }
                        }
                    }

                    // сохранить статистику в report.txt
                    using StreamWriter writer = new(_reportPath, false);
                    foreach (var kv in keyCounts.OrderByDescending(kv => kv.Value))
                    {
                        writer.WriteLine($"{kv.Key}: {kv.Value} times");
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine($"[Analyzer] Analysis error: {e.Message}");
                }
            }
        });

        thread.IsBackground = true;
        thread.Start();
    }

    /// <summary>
    /// Останавливает анализатор (вызвать перед выходом из игры).
    /// </summary>
    public void Stop() => _running = false;
}

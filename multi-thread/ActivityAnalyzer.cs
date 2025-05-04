using System.Diagnostics;

namespace GameEngine.multi_thread;

public class ActivityAnalyzer
{
    private readonly string _logPath;
    private readonly string _reportPath;
    private bool _running = true;

    public ActivityAnalyzer(string logPath = "log.txt", string reportPath = "report.txt")
    {
        _logPath = logPath;
        _reportPath = reportPath;
    }
    
    public void Start()
    {
        Thread thread = new(() =>
        {
            while (_running)
            {
                Thread.Sleep(10000); // анализ каждые 10 секунд

                if (!File.Exists(_logPath)) {continue;}

                var lines = File.ReadAllLines(_logPath);
                var keyCounts = new Dictionary<string, int>();

                foreach (var line in lines)
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

                using StreamWriter writer = new(_reportPath, false);
                foreach (var kv in keyCounts.OrderByDescending(kv => kv.Value))
                {
                    writer.WriteLine($"{kv.Key}: {kv.Value} times");
                }
            }
        });

        thread.IsBackground = true;
        thread.Start();
    }

    public void Stop() => _running = false;
}
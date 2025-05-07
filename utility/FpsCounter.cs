using System.Diagnostics;

namespace GameEngine.utility;

public class FpsCounter
{
    private int _frames;
    private float _elapsed;
    private int _fps;
    private readonly Stopwatch _stopwatch;

    public FpsCounter()
    {
        _stopwatch = Stopwatch.StartNew();
    }

    public void FrameTick()
    {
        _frames++;
        _elapsed += (float)_stopwatch.Elapsed.TotalSeconds;
        _stopwatch.Restart();

        if (_elapsed >= 1f)
        {
            _fps = _frames;
            _frames = 0;
            _elapsed = 0f;
        }
    }

    public int GetFps()
    {
        return _fps;
    }
}
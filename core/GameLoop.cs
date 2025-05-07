using System.Diagnostics;
using GameEngine.components;
using GameEngine.graphics;
using GameEngine.interfaces;
using GameEngine.utility;

namespace GameEngine.core;

/// <summary>
/// Игровой цикл. Вызывает обновление и отображение каждый кадр.
/// </summary>
public class GameLoop
{
    private readonly IScene _scene;
    private readonly Renderer _renderer;
    private readonly CameraComponent? _camera;
    private readonly int _frameDelay;
    private readonly FpsCounter _fpsCounter = new();
    
    private bool _running = true;

    public GameLoop(IScene scene, Renderer renderer, CameraComponent? camera = null, int targetFPS = 10)
    {
        _scene = scene;
        _renderer = renderer;
        _camera = camera;
        _frameDelay = targetFPS;
    }

    public void Stop()
    {
        _running = false;
    }
    
    public void Run()
    {
        _scene.Load();
        var stopwatch = new Stopwatch();

        while (_running)
        {
            stopwatch.Restart();
        
            _fpsCounter.FrameTick();
            _camera?.Update();
            _scene.Update();
            _renderer.RenderFrame(_fpsCounter.GetFps());

            stopwatch.Stop();
            int elapsed = (int)stopwatch.ElapsedMilliseconds;
            int sleepTime = _frameDelay - elapsed;

            if (sleepTime > 0)
            {
                Thread.Sleep(sleepTime);
            }
        }
    }
}
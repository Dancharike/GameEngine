using GameEngine.graphics;
using GameEngine.interfaces;

namespace GameEngine.core;

/// <summary>
/// Игровой цикл. Вызывает обновление и отображение каждый кадр.
/// </summary>
public class GameLoop
{
    private readonly IScene _scene;
    private readonly Renderer _renderer;
    private readonly int _frameDelay;

    private bool _running = true;

    public GameLoop(IScene scene, Renderer renderer, int targetFPS = 60)
    {
        _scene = scene;
        _renderer = renderer;
        _frameDelay = 1000 / targetFPS;
    }

    public void Stop()
    {
        _running = false;
    }
    
    public void Run()
    {
        _scene.Load();

        while (_running)
        {
            _scene.Update();
            _renderer.RenderFrame();
            Thread.Sleep(_frameDelay);
        }
    }
}
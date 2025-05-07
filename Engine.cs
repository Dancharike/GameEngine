using GameEngine.core;
using GameEngine.graphics;
using GameEngine.interfaces;
using GameEngine.window;

namespace GameEngine;

/// <summary>
/// Чё-то по типу точки инициализации движка. 
/// </summary>
public class Engine
{
    public void Start(IScene scene, string title, int width, int height)
    {
        var window = new WindowHost(title, width, height);
        
        if (scene is not IRenderSource renderSource)
        {
            throw new InvalidOperationException("Scene must implement IRenderSource to support rendering.");
        }
        
        var camera = scene is ICameraProvider provider ? provider.GetCamera() : null;
        var renderer = new Renderer(window, renderSource, camera);
        var gameLoop = new GameLoop(scene, renderer, camera);
        
        // передача ссылки на окно внутрь сцены
        if (scene is DemoGame demoGame)
        {
            demoGame.SetWindow(window);
            demoGame.SetRenderer(renderer);
        }
        
        // остановить текущий GameLoop при закрытии окна приложения
        window.FormClosed += (_, _) =>
        {
            gameLoop.Stop();
            Environment.Exit(0);
        };
        
        // инициализация Game-контекста
        Game.Initialize(scene, window, renderer);
        
        // запуск окошка игры
        Application.Run(window);
    }
}
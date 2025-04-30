using GameEngine.core;
using GameEngine.graphics;
using GameEngine.interfaces;
using GameEngine.shapes;
using GameEngine.window;

namespace GameEngine;

/// <summary>
/// Чё-то по типу точки инициализации движка. 
/// </summary>
public class Engine
{
    public void Start(IScene scene, string title, int width, int height)
    {
        var shapeManager = new ShapeManager();
        var window = new WindowHost(title, width, height);
        var renderer = new Renderer(window, shapeManager);

        if (scene is IUsesShapeManager smScene)
        {
            smScene.SetShapeManager(shapeManager);
        }

        Thread gameThread = new(() =>
        {
            var loop = new GameLoop(scene, renderer);
            loop.Run();
        });
        
        gameThread.Start();
        Application.Run(window);
    }
}
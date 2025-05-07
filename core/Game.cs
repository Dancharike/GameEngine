using GameEngine.graphics;
using GameEngine.interfaces;
using GameEngine.utility;
using GameEngine.window;

namespace GameEngine.core;

/// <summary>
/// Класс с заранее добавленными методами/функциями для более удобного использования, ускорения разработки конечным пользователем.
/// </summary>
public static class Game
{
    private static IScene _startScene;
    private static IScene _currentScene;
    private static Renderer _renderer;
    private static WindowHost _window;
    private static GameLoop _gameLoop;

    public static void Initialize(IScene startScene, WindowHost window, Renderer renderer)
    {
        _startScene = startScene;
        _currentScene = startScene;
        _window = window;
        _renderer = renderer;
        
        _gameLoop = new GameLoop(_currentScene, renderer);
        StartGameLoop();
    }

    private static void StartGameLoop()
    {
        Thread gameThread = new(() =>
        {
            _gameLoop.Run();
        });
        gameThread.Start();
    }
    
    /// <summary>
    /// Мгновенное завершение приложения.
    /// </summary>
    public static void Exit() {Environment.Exit(0);}

    /// <summary>
    /// Перезагрузка активной сцены.
    /// Сцена загружается заново откатывая все изменения произошедшие до вызова этого метода.
    /// </summary>
    public static void SceneRestart()
    {
        _gameLoop.Stop();
        EventBus.ClearAll();
        
        _currentScene = (IScene)Activator.CreateInstance(_currentScene.GetType())!;

        if (_currentScene is DemoGame demoGame)
        {
            demoGame.SetWindow(_window);
            demoGame.SetRenderer(_renderer);
        }
        
        _renderer.SetRenderSource((IRenderSource)_currentScene);
        _gameLoop = new GameLoop(_currentScene, _renderer);
        StartGameLoop();
    }

    /// <summary>
    /// Перезагрузка игры.
    /// Все изменения, произошедшие в игре, будут сброшены (если нет активной записи сохранений).
    /// Игра вернётся к самой первой сцене во всей игре.
    /// </summary>
    public static void GameRestart()
    {
        _gameLoop.Stop();
        EventBus.ClearAll();
        
        _currentScene = (IScene)Activator.CreateInstance(_startScene.GetType())!;
        
        if (_currentScene is DemoGame demoGame)
        {
            demoGame.SetWindow(_window);
            demoGame.SetRenderer(_renderer);
        }
        
        _renderer.SetRenderSource((IRenderSource)_currentScene);
        _gameLoop = new GameLoop(_currentScene, _renderer);
        StartGameLoop();
    }
    
    /// <summary>
    /// Метод, что чаще всего используется для плавности передвижения объекта 
    /// </summary>
    public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
    {
        float one = a.X + (b.X - a.X) * t;
        float two = a.Y + (b.Y - a.Y) * t;
        
        return new Vector2(one, two);
    }
}
using GameEngine.assets.player;
using GameEngine.core;
using GameEngine.events;
using GameEngine.interfaces;
using GameEngine.multi_thread;
using GameEngine.shapes;
using GameEngine.utility;

namespace GameEngine;

public class DemoGame : IScene, IRenderSource
{
    private List<GameObject> _objects = new();
    private ActivityAnalyzer _analyzer;
    
    public void Load()
    {
        _analyzer = new ActivityAnalyzer();
        _analyzer.Start();
        
        // здесь можно на разные кнопки с клавиатуры назначать разную логику кода
        EventBus.Subscribe<KeyPressedEvent>(e =>
        {
            if (e.Key == Keys.Escape)
            {
                Log.Event("Exiting the game.");
                Game.Exit();
                _analyzer.Stop();
            }

            if (e.Key == Keys.R)
            {
                Log.Event("Restarting the scene...");
                Game.SceneRestart();
            }

            if (e.Key == Keys.Z)
            {
                Log.Event("Restarting the game...");
                Game.GameRestart();
            }
            
        });

        var player = new Player(new Vector2(10, 10));
        _objects.Add(player);
    }

    public void Update()
    {
        foreach (var obj in _objects)
        {
            obj.Update();
        }
        
        KeyLogger.Flush();
    }

    public IEnumerable<IRender> GetRenderables()
    {
        return _objects;
    }
}
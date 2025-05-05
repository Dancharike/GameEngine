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
    private GameObject _player;
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
        
        var collider = new BoxCollider(new Vector2(10, 10), new Vector2(50, 50));
        //var sprite = SpriteLoader.LoadSprite("spr_player");
        var sprite = SpriteLoader.LoadSpriteFromFrames("spr_player_down", 6, 12);

        _player = new GameObject("Player", collider, sprite);
        _objects.Add(_player);
    }

    public void Update()
    {
        float speed = 2f;

        if (InputManager.IsKeyPressed(Keys.W))
        {
            _player.Position.Y -= speed;
            KeyLogger.Log(Keys.W);
        }

        if (InputManager.IsKeyPressed(Keys.S))
        {
            _player.Position.Y += speed;
            KeyLogger.Log(Keys.S);
        }

        if (InputManager.IsKeyPressed(Keys.A))
        {
            _player.Position.X -= speed;
            KeyLogger.Log(Keys.A);
        }

        if (InputManager.IsKeyPressed(Keys.D))
        {
            _player.Position.X += speed;
            KeyLogger.Log(Keys.D);
        }
        
        KeyLogger.Flush();
    }

    public IEnumerable<IRender> GetRenderables()
    {
        return _objects;
    }
}
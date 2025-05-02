using GameEngine.core;
using GameEngine.events;
using GameEngine.interfaces;
using GameEngine.shapes;
using GameEngine.utility;
using GameEngine.visuals;

namespace GameEngine;

public class DemoGame : IScene, IRenderSource
{
    private List<GameObject> _objects = new();
    private GameObject _player;
    
    public void Load()
    {
        // здесь можно на разные кнопки с клавиатуры назначать разную логику кода
        EventBus.Subscribe<KeyPressedEvent>(e =>
        {
            if (e.Key == Keys.Escape)
            {
                Log.Event("Exiting the game.");
                Game.Exit();
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
        var visual = new BoxVisual(Color.Red);

        _player = new GameObject("Player", collider, visual);
        _objects.Add(_player);
    }

    public void Update()
    {
        float speed = 2f;
        
        if(InputManager.IsKeyPressed(Keys.W)) {_player.Position.Y -= speed;}
        if(InputManager.IsKeyPressed(Keys.S)) {_player.Position.Y += speed;}
        if(InputManager.IsKeyPressed(Keys.A)) {_player.Position.X -= speed;}
        if(InputManager.IsKeyPressed(Keys.D)) {_player.Position.X += speed;}
    }

    public IEnumerable<IRender> GetRenderables()
    {
        return _objects;
    }
}
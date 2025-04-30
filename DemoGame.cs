using GameEngine.interfaces;
using GameEngine.shapes;
using GameEngine.utility;

namespace GameEngine;

public class DemoGame : IScene, IUsesShapeManager
{
    private ShapeManager _shapes;
    private Shape2D _player;

    public void SetShapeManager(ShapeManager manager)
    {
        _shapes = manager;
    }

    public void Load()
    {
        _player = new Shape2D(new Vector2(10, 10), new Vector2(50, 50), "Player", Color.Red);
        _shapes.Add(_player);
    }

    public void Update()
    {
        float speed = 2f;
        
        if(InputManager.IsKeyPressed(Keys.W)) {_player.Position.Y -= speed;}
        if(InputManager.IsKeyPressed(Keys.S)) {_player.Position.Y += speed;}
        if(InputManager.IsKeyPressed(Keys.A)) {_player.Position.X -= speed;}
        if(InputManager.IsKeyPressed(Keys.D)) {_player.Position.X += speed;}
    }
}
using GameEngine.core;
using GameEngine.multi_thread;
using GameEngine.shapes;
using GameEngine.utility;

namespace GameEngine.assets.player;

public class Player : GameObject
{
    private ActivityAnalyzer _analyzer;
    
    public Player(Vector2 position) : base 
    (
        "Player",
        new BoxCollider(position, new Vector2(50, 50)),
        SpriteLoader.LoadSpriteFromFrames("spr_player_down", 6, 12)
    ) {}

    public override void OnUpdate()
    {
        float speed = 2f;

        if (InputManager.IsKeyPressed(Keys.W))
        {
            Position.Y -= speed;
            KeyLogger.Log(Keys.W);
        }

        if (InputManager.IsKeyPressed(Keys.S))
        {
            Position.Y += speed;
            KeyLogger.Log(Keys.S);
        }

        if (InputManager.IsKeyPressed(Keys.A))
        {
            Position.X -= speed;
            KeyLogger.Log(Keys.A);
        }

        if (InputManager.IsKeyPressed(Keys.D))
        {
            Position.X += speed;
            KeyLogger.Log(Keys.D);
        }
        
        KeyLogger.Flush();
    }
}
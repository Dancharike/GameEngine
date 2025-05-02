using GameEngine.utility;

namespace GameEngine.interfaces;

public interface ICollider
{
    Vector2 Position { get; set; }
    Vector2 Size { get; set; }
    
    bool CheckCollision(ICollider other);
}
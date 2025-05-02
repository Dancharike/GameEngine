using GameEngine.interfaces;
using GameEngine.utility;

namespace GameEngine.shapes;

public class BoxCollider : ICollider
{
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }

    public BoxCollider(Vector2 position, Vector2 size)
    {
        Position = position;
        Size = size;
    }

    public bool CheckCollision(ICollider other)
    {
        return 
            !(Position.X + Size.X < other.Position.X ||
              Position.X > other.Position.X + other.Size.X ||
              Position.Y + Size.Y < other.Position.Y ||
              Position.Y > other.Position.Y + other.Size.Y);
    }
}
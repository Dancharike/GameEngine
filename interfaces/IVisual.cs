using GameEngine.utility;

namespace GameEngine.interfaces;

public interface IVisual
{
    void Render(Graphics g, Vector2 position, Vector2 size);
}
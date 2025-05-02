using GameEngine.interfaces;
using GameEngine.utility;

namespace GameEngine.visuals;

public class BoxVisual : IVisual
{
    public Color Color { get; set; }

    public BoxVisual(Color color)
    {
        Color = color;
    }

    public void Render(Graphics g, Vector2 position, Vector2 size)
    {
        using SolidBrush brush = new(Color);
        g.FillRectangle(brush, position.X, position.Y, size.X, size.Y);
    }
}
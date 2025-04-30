using GameEngine.interfaces;
using GameEngine.utility;

namespace GameEngine.shapes;

public class Shape2D : IRender
{
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; }
    public string Tag { get; set; }
    public Color Color { get; set; } = Color.White;

    public Shape2D(Vector2 position, Vector2 scale, string tag = "", Color? color = null)
    {
        Position = position;
        Scale = scale;
        Tag = tag;
        Color = color ?? Color.White;
        
        Log.Info($"Shape2D ({Tag}) created!");
    }

    public void Render(Graphics g)
    {
        using SolidBrush brush = new(Color);
        g.FillRectangle(brush, Position.X, Position.Y, Scale.X, Scale.Y);
    }
}
using System.Drawing.Drawing2D;
using GameEngine.interfaces;
using GameEngine.utility;

namespace GameEngine.visuals;

/// <summary>
/// Класс содержащий метод для рисования спрайта.
/// </summary>
public class Sprite : ISprite
{
    private readonly Image _image;

    public Sprite(Image image)
    {
        _image = image;
    }

    public void Draw(Graphics g, Vector2 position, Vector2 size)
    {
        g.InterpolationMode = InterpolationMode.NearestNeighbor;
        g.DrawImage(_image, (int)position.X, (int)position.Y, (int)size.X, (int)size.Y );
    }

    public Vector2 GetSize()
    {
        return new Vector2(_image.Width, _image.Height);
    }
}
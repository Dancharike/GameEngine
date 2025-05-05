using GameEngine.interfaces;
using GameEngine.utility;

namespace GameEngine.visuals;

/// <summary>
/// Класс, что добавляет возможность использовать лист спрайтов для анимированных спрайтов.
/// </summary>
public class AnimatedSprite : ISprite
{
    private readonly List<Image>? _frames;
    private readonly Image? _sheet;
    
    private readonly int _frameWidth;
    private readonly int _frameHeight;
    private readonly int _frameCount;
    private readonly int _frameDelay;
    
    private int _currentFrame = 0;
    private int _frameCounter = 0;

    /// <summary>
    /// Конструктор использующийся для анимации спрайт листа.
    /// </summary>
    public AnimatedSprite(Image sheet, int frameCount, int framesPerSecond = 30, int gameFPS = 60)
    {
        _sheet = sheet;
        _frameCount = frameCount;
        _frameWidth = sheet.Width / frameCount;
        _frameHeight = sheet.Height;
        _frameDelay = Math.Max(1, gameFPS / framesPerSecond);
    }
    
    /// <summary>
    /// Конструктор использующийся для анимации из набора отдельных кадров.
    /// </summary>
    public AnimatedSprite(List<Image> frames, int framesPerSecond = 30)
    {
        _frames = frames;
        _frameCount = frames.Count;
        _frameDelay = Math.Max(1, 60 / framesPerSecond);
    }


    public void Draw(Graphics g, Vector2 position, Vector2 size)
    {
        if (_sheet != null)
        {
            // отрисовка из спрайт-листа
            Rectangle srcRect = new Rectangle(_currentFrame * _frameWidth, 0, _frameWidth, _frameHeight);
            Rectangle destRect = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            g.DrawImage(_sheet, destRect, srcRect, GraphicsUnit.Pixel);
        }
        else if (_frames != null)
        {
            // отрисовка из набора изображений
            var image = _frames[_currentFrame];
            g.DrawImage(image, (int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        _frameCounter++;
        if (_frameCounter >= _frameDelay)
        {
            _frameCounter = 0;
            _currentFrame = (_currentFrame + 1) % _frameCount;
        }
    }
}
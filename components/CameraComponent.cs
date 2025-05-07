using GameEngine.core;
using GameEngine.utility;
using GameEngine.window;

namespace GameEngine.components;

/// <summary>
/// Компонент камеры прикрепляемый к объекту.
/// По умолчанию фиксирован на игровом объекте.
/// </summary>
public class CameraComponent
{
    public Vector2 Position { get; protected set; }
    public static CameraComponent? Current { get; set; }
    public Vector2 ViewSize { get; }
    public float Scale { get; }
    
    private readonly GameObject _target;
    private readonly WindowHost _window;

    private const float _followSpeed = 0.1f;

    public CameraComponent(GameObject target, WindowHost window, Vector2 viewportSize)
    {
        _target = target;
        _window = window;
        ViewSize = viewportSize;

        Position = _target.Position + (_target.Size / 2);
        Scale = CalculateScale();
        Current = this;
    }

    private float CalculateScale()
    {
        float scaleX = _window.ClientSize.Width / ViewSize.X;
        float scaleY = _window.ClientSize.Height / ViewSize.Y;
        
        // равномерный масштаб
        return MathF.Min(scaleX, scaleY);
    }

    /// <summary>
    /// Виртуальный метод с простой реализацией жёстко прикреплённой камерой к игровому объекту, к которому этот компонент прикреплён.
    /// </summary>
    public virtual void Update()
    {
        var targetCenter = _target.Position + (_target.Size / 2f);
        Position = Game.Lerp(Position, targetCenter, _followSpeed);
    }
    
    public float GetScale()
    {
        return Scale;
    }
}
namespace GameEngine.utility;

/// <summary>
/// Базовый двумерный вектор.
/// </summary>
public class Vector2
{
    public float X { get; set; }
    public float Y { get; set; }

    public Vector2() : this(0f, 0f) {}
    
    public Vector2(float x, float y)
    {
        X = x;
        Y = y;
    }
    
    public override string ToString() => $"({X}, {Y})";
}
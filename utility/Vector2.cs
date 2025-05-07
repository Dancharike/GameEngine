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
    
    public static Vector2 operator +(Vector2 a, Vector2 b)
    {
        return new Vector2(a.X + b.X, a.Y + b.Y);
    }
    
    public static Vector2 operator -(Vector2 a, Vector2 b)
    {
        return new Vector2(a.X - b.X, a.Y - b.Y);
    }
    
    public static Vector2 operator /(Vector2 v, float scalar)
    {
        return new Vector2(v.X / scalar, v.Y / scalar);
    }
    
    public static Vector2 operator *(Vector2 v, float scalar)
    {
        return new Vector2(v.X * scalar, v.Y * scalar);
    }
    
    public override string ToString() => $"({X}, {Y})";
}
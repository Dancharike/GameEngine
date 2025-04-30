namespace GameEngine.utility;

/// <summary>
/// Отслеживание нажатых клавиш.
/// </summary>
public class InputManager
{
    private static readonly HashSet<Keys> _pressedKeys = new();

    public static void RegisterKeyDown(Keys key)
    {
        _pressedKeys.Add(key);
    }

    public static void RegisterKeyUp(Keys key)
    {
        _pressedKeys.Remove(key);
    }

    public static bool IsKeyPressed(Keys key)
    {
        return _pressedKeys.Contains(key);
    }
}
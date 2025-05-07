using GameEngine.utility;

namespace GameEngine.window;

/// <summary>
/// Главное окно игры. Представляет собой форму с двойной буферизацией.
/// </summary>
public class WindowHost : Form
{
    public WindowHost(string title, int width, int height)
    {
        Text = title;
        Width = width;
        Height = height;
        DoubleBuffered = true;
        
        // полноэкранный режим
        FormBorderStyle = FormBorderStyle.None;
        WindowState = FormWindowState.Maximized;
        
        // обработка клавиатуры
        KeyPreview = true;
        KeyDown += (s, e) => InputManager.RegisterKeyDown(e.KeyCode);
        KeyUp += (s, e) => InputManager.RegisterKeyUp(e.KeyCode);
    }
}
using GameEngine.shapes;
using GameEngine.window;

namespace GameEngine.graphics;

/// <summary>
/// Отвечает за отображение всех фигур на окне.
/// </summary>
public class Renderer
{
    private readonly WindowHost _window;
    private readonly ShapeManager _shapeManager;

    public Renderer(WindowHost window, ShapeManager shapeManager)
    {
        _window = window;
        _shapeManager = shapeManager;

        // подписка на событие отображения окна
        _window.Paint += OnPaint;
    }

    private void OnPaint(object? sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.Clear(Color.Black); // цвет фона по умолчанию

        // вызов метода Render() у каждого существующего объекта
        foreach (var shape in _shapeManager.GetAll())
        {
            shape.Render(g);
        }
    }

    /// <summary>
    /// Вызывает перерисовку окна (асинхронно с потока).
    /// </summary>
    public void RenderFrame()
    {
        _window.Invoke((MethodInvoker)(() => _window.Refresh()));
    }
}
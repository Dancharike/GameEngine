using GameEngine.interfaces;
using GameEngine.window;

namespace GameEngine.graphics;

/// <summary>
/// Отвечает за отображение всех фигур на окне.
/// </summary>
public class Renderer
{
    private readonly WindowHost _window;
    private IRenderSource _renderSource;

    public Renderer(WindowHost window, IRenderSource renderSource)
    {
        _window = window;
        _renderSource = renderSource;

        // подписка на событие отображения окна
        _window.Paint += OnPaint;
    }

    private void OnPaint(object? sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.Clear(Color.Black); // цвет фона по умолчанию

        // вызов метода Render() у каждого существующего объекта
        foreach (var renderable in _renderSource.GetRenderables())
        {
            renderable.Render(g);
        }
    }

    /// <summary>
    /// Вызывает перерисовку окна (асинхронно с потока).
    /// </summary>
    public void RenderFrame()
    {
        try
        {
            if (!_window.IsDisposed)
            {
                _window.Invoke((MethodInvoker)(() => _window.Refresh()));
            }
        }
        catch (ObjectDisposedException)
        {
            // окно не закрыто - игнорировать
        }
        catch (InvalidOperationException)
        {
            // поток закрыт - игнорировать
        }
    }

    public void SetRenderSource(IRenderSource source)
    {
        _renderSource = source;
    }
}
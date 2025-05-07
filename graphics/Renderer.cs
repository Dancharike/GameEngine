using GameEngine.components;
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
    private CameraComponent? _camera;
    
    private int _currentFps;

    public Renderer(WindowHost window, IRenderSource renderSource, CameraComponent? camera = null)
    {
        _window = window;
        _renderSource = renderSource;
        _camera = camera;

        if (camera != null)
        {
            CameraComponent.Current = _camera;
        }

        // подписка на событие отображения окна
        _window.Paint += OnPaint;
    }
    
    private void OnPaint(object? sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.Clear(Color.Black); // цвет фона по умолчанию

        if (_camera != null)
        {
            float scale = _camera.GetScale();
            g.ScaleTransform(scale, scale);
            
            var halfView = _camera.ViewSize / 2f;
            var centerOffset = _camera.Position - halfView;
            
            // центрирование камеры
            g.TranslateTransform(-centerOffset.X, -centerOffset.Y);
        }

        // вызов метода Render() у каждого существующего объекта
        foreach (var renderable in _renderSource.GetRenderables())
        {
            renderable.Render(g);
        }
        
        g.ResetTransform();

        if (_currentFps >= 0)
        {
            using var font = new Font("Consolas", 12f, FontStyle.Bold);
            using var brush = new SolidBrush(Color.Green);
            g.DrawString($"FPS: {_currentFps}", font, brush, 10, 10);
        }
    }

    /// <summary>
    /// Вызывает перерисовку окна (асинхронно с потока).
    /// </summary>
    public void RenderFrame(int fps = -1)
    {
        _currentFps = fps;
        try
        {
            if (!_window.IsDisposed)
            {
                _window.Invalidate();
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

    public void SetCamera(CameraComponent camera)
    {
        _camera = camera;
        CameraComponent.Current = camera;
    }
}
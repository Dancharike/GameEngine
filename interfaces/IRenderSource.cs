namespace GameEngine.interfaces;

/// <summary>
/// Интерфейс источника объектов, которые могут быть отрисованы.
/// </summary>
public interface IRenderSource
{
    IEnumerable<IRender> GetRenderables();
}
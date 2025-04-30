using GameEngine.interfaces;

namespace GameEngine.shapes;

/// <summary>
/// Хранит и управляет всеми объектами, которые нужно рисовать.
/// </summary>
public class ShapeManager
{
    private readonly List<IRender> _shapes = new();

    public void Add(IRender shape)
    {
        _shapes.Add(shape);
    }

    public void Remove(IRender shape)
    {
        _shapes.Remove(shape);
    }

    public IEnumerable<IRender> GetAll()
    {
        return _shapes;
    }
}